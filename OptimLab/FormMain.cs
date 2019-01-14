using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using PlotComponentsGDI;

namespace OptimLab
{
    public struct Experiment
    {
        public string MethodName;
        public string MethodVisibleName;
        public MethodOptions MethodOptions;
    }

    public partial class FormMain : Form
    {
        private Dictionary<string, Type> methods;
        private List<Experiment> experiments;

        private GrishaginFunction targetFunction;
        private List<Constraint> constraints;
        private MethodOptions methodOptions;

        private ToolStripMenuItem menuItemCurrentMethod;
        private string currentMethodName;
        private Method method;

        private PlotBuilder plotBuilder;

        private static readonly Color[] StandardColors = new Color[]
            {
                Color.Red,
                Color.Lime,
                Color.Blue,
                Color.Yellow,
                Color.Cyan,
                Color.Magenta,
                Color.Brown,
                Color.Gainsboro,
                Color.Goldenrod,
                Color.Honeydew
            };

        public FormMain()
        {
            InitializeComponent();
            MinimumSize = new Size(640, 480);

            methods = new Dictionary<string, Type>();
            experiments = new List<Experiment>();

            FileInfo[] files = new DirectoryInfo(Application.StartupPath).GetFiles();

            foreach (FileInfo file in files)
            {
                try
                {
                    Assembly assembly = Assembly.LoadFile(file.FullName);
                    Type[] types = assembly.GetTypes();

                    foreach (Type type in types)
                    {
                        if (type.IsSubclassOf(typeof(Method)) && !methods.ContainsKey(type.Name))
                            methods.Add(type.Name, type);
                    }
                }
                catch (BadImageFormatException ex)
                {
                }
            }

            Dictionary<string, ToolStripMenuItem> menuItemGroups = new Dictionary<string, ToolStripMenuItem>();
            foreach (Type type in methods.Values)
            {
                string groupName = ((Method)Activator.CreateInstance(type)).GetGroupName();

                if (!menuItemGroups.ContainsKey(groupName))
                {
                    ToolStripMenuItem menuItemGroup = new ToolStripMenuItem();
                    menuItemGroup.Name = "toolStripMenuItemGroup" + groupName;
                    menuItemGroup.Text = groupName;
                    menuItemGroups.Add(groupName, menuItemGroup);
                }
            }

            bool isFirstMethod = true;
            foreach (String methodName in methods.Keys)
            {
                ToolStripMenuItem menuItem = new ToolStripMenuItem();
                menuItem.Name = "toolStripMenuItem" + methodName;
                menuItem.Text = ((Method)Activator.CreateInstance(methods[methodName])).GetMethodName();
                menuItem.Click += new EventHandler(toolStripMenuItemAnyMethod_Click);

                if (isFirstMethod)
                {
                    menuItemCurrentMethod = menuItem;
                    menuItemCurrentMethod.PerformClick();
                    isFirstMethod = false;
                }

                string groupName = ((Method)Activator.CreateInstance(methods[methodName])).GetGroupName();
                menuItemGroups[groupName].DropDownItems.Add(menuItem);
            }

            int currentPosition = 0;
            foreach (ToolStripMenuItem menuItem in menuItemGroups.Values)
            {
                toolStripMenuItemMethod.DropDownItems.Insert(currentPosition, menuItem);
                currentPosition++;
            }

            targetFunction = new GrishaginFunction(1);
            constraints = new List<Constraint>();

            plotBuilder = new PlotBuilder();
            plotBuilder.ShowAxises = false;
            plotBuilder.ShowNumbers = false;
            SetSizesAndLocations();
            Controls.Add(plotBuilder);
            ResetControls();
        }

        private void SetSizesAndLocations()
        {
            plotBuilder.Width = plotBuilder.Height = Height - 70;
            plotBuilder.Location = new Point(5, 30);

            labelIterations.Location = new Point(5 + plotBuilder.Width + 10, 50);
            labelPoint.Location = new Point(5 + plotBuilder.Width + 10, 100);
            labelValue.Location = new Point(5 + plotBuilder.Width + 10, 120);
            labelErrorInPoint.Location = new Point(5 + plotBuilder.Width + 10, 150);
            labelErrorInValue.Location = new Point(5 + plotBuilder.Width + 10, 170);
            labelEvalsConst1.Location = new Point(5 + plotBuilder.Width + 10, 200);
            labelEvalsConst2.Location = new Point(5 + plotBuilder.Width + 10, 220);
            labelEvalsConst3.Location = new Point(5 + plotBuilder.Width + 10, 240);
            labelEvalsTarget.Location = new Point(5 + plotBuilder.Width + 10, 260);
        }

        private void ResetControls()
        {
            labelIterations.Text = OptimLab.Properties.Resources.LabelIterationsText + ": 0";
            labelPoint.Text = OptimLab.Properties.Resources.LabelPointText + ":";
            labelValue.Text = OptimLab.Properties.Resources.LabelValueText + ":";
            labelErrorInPoint.Text = OptimLab.Properties.Resources.LabelErrorInPointText + ":";
            labelErrorInValue.Text = OptimLab.Properties.Resources.LabelErrorInValueText + ":";
            labelEvalsTarget.Text = OptimLab.Properties.Resources.LabelEvalsTargetText + ":";
            labelEvalsConst1.Text = OptimLab.Properties.Resources.LabelEvalsConst1Text + ":";
            labelEvalsConst2.Text = OptimLab.Properties.Resources.LabelEvalsConst2Text + ":";
            labelEvalsConst3.Text = OptimLab.Properties.Resources.LabelEvalsConst3Text + ":";

            Color[] constraintColors = new Color[] { Color.DarkGray, Color.Pink, Color.Cyan };

            plotBuilder.RemoveAllPlots();
            plotBuilder.RemoveAllConstraints();

            Plot plot = Triangulator.SurfaceLevelLines(
                delegate(double x, double y)
                {
                    return targetFunction.CalculateFunction(new double[] { x, y });
                },
                0.0, 1.0, 0.0, 1.0, 100, 100, 10);
            plot.Information.Name = "Target function";
            plot.Color = Color.Black;
            plotBuilder.AddPlot(plot);

            for (int i = 0; i < constraints.Count; i++)
            {
                plotBuilder.ConstraintColor = constraintColors[i];
                plotBuilder.AddConstraint("Constraint " + (i + 1).ToString(),
                    delegate(double x, double y)
                    {
                        return constraints[i].A * constraints[i].F.CalculateFunction(new double[] { x, y }) +
                            constraints[i].B;
                    }
                );
            }

            plotBuilder.PictureLocation = new PointF((float)targetFunction.MaximumPoint[0],
                (float)targetFunction.MaximumPoint[1]);
        }

        public void GenerateConstraint()
        {
            Constraint constraint = new Constraint();
            Random random = new Random();

            constraint.A = 0.0;
            constraint.B = 0.0;
            constraint.F = new GrishaginFunction(1);

            while (constraint.A * constraint.F.CalculateFunction(targetFunction.MaximumPoint)
                + constraint.B >= 0.0)
            {
                constraint.A = random.NextDouble() + 1.0;
                constraint.B = -random.NextDouble() * 2.0 - 3.0;

                switch (constraints.Count)
                {
                    case 0:
                        constraint.F = new GrishaginFunction(random.Next(1, 34));
                        break;
                    case 1:
                        constraint.F = new GrishaginFunction(random.Next(34, 68));
                        break;
                    case 2:
                        constraint.F = new GrishaginFunction(random.Next(68, 101));
                        break;
                }
            }
            constraints.Add(constraint);
        }

        private void DrawOperationProperty(int index, Color color, PlotBuilder graph)
        {
            int[] indices = new int[100];
            int[] numIterations = new int[100];

            for (int i = 0; i < 100; i++)
                indices[i] = i;

            Cursor = Cursors.WaitCursor;
            for (int i = 0; i < 100; i++)
            {
                Problem problem = new Problem();
                problem.Left = new double[] { 0.0, 0.0 };
                problem.Right = new double[] { 1.0, 1.0 };
                problem.Constraints = new List<FunctionDelegate>();

                GrishaginFunction targetFunction = new GrishaginFunction(i + 1);
                problem.TargetFunction = delegate(double[] args)
                {
                    return -targetFunction.CalculateFunction(args);
                };

                method = (Method)Activator.CreateInstance(methods[experiments[index].MethodName]);
                method.SetProblem(problem);
                method.SetOptions(experiments[index].MethodOptions);
                method.IsOperationProperty = true;
                method.MaximumPoint = targetFunction.MaximumPoint;
                method.Solve();
                Text = "Выполнено: " + i + "%";

                numIterations[i] = method.GetTrials().Count;
            }
            Cursor = Cursors.Default;
            Text = "OptimLab";

            Array.Sort(numIterations, indices);

            PointF[] points = new PointF[100];
            for (int i = 0; i < 100; i++)
                points[i] = new PointF(numIterations[i], i + 1);

            Plot plotPoints = Triangulator.PointsVertexArray(points);
            plotPoints.Information.Name = "Points" + index;
            plotPoints.Color = color;
            graph.AddPlot(plotPoints);

            Plot plotLines = Triangulator.CurveStripVertexArray(points);
            plotLines.Information.Name = "Lines" + index;
            plotLines.Color = color;
            graph.AddPlot(plotLines);
        }

        private void toolStripMenuItemAnyMethod_Click(object sender, EventArgs e)
        {
            menuItemCurrentMethod.Checked = false;
            menuItemCurrentMethod = (ToolStripMenuItem)sender;
            menuItemCurrentMethod.Checked = true;

            currentMethodName = menuItemCurrentMethod.Name.Substring(17);
            methodOptions = ((Method)Activator.CreateInstance(methods[currentMethodName])).GetDefaultOptions();
            toolStripMenuItemSaveResults.Enabled = false;
        }

        private void toolStripMenuItemSaveResults_Click(object sender, EventArgs e)
        {
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.InitialDirectory = Application.StartupPath;
            dialog.Filter = "Файлы с результатами (*.opt) | *.opt";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                StreamWriter file = new StreamWriter(dialog.FileName);
                file.WriteLine(targetFunction.Index);
                file.WriteLine(constraints.Count);
                for (int i = 0; i < constraints.Count; i++)
                {
                    file.Write(constraints[i].A);
                    file.Write(" ");
                    file.Write(constraints[i].B);
                    file.Write(" ");
                    file.Write(constraints[i].F.Index);
                    file.WriteLine();
                }

                List<Trial> trials = method.GetTrials();
                file.WriteLine(trials.Count);
                int iteration = 1;
                foreach (Trial trial in trials)
                {
                    file.Write(iteration);
                    file.Write(" ");
                    file.Write(trial.Y[0].ToString("F5", CultureInfo.InvariantCulture));
                    file.Write(" ");
                    file.Write(trial.Y[1].ToString("F5", CultureInfo.InvariantCulture));
                    file.Write(" ");
                    file.Write(trial.Index);
                    file.Write(" ");
                    file.Write(trial.Evals[Math.Max(trial.Index, 0)].ToString("F5", CultureInfo.InvariantCulture));
                    file.WriteLine();
                    iteration++;
                }
                file.Close();
            }
        }

        private void toolStripMenuItemOpenResults_Click(object sender, EventArgs e)
        {
            FormOpenResults dialog = new FormOpenResults();
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                StreamReader file = new StreamReader(dialog.FileName);
                string temp = "";

                temp = file.ReadLine();
                targetFunction = new GrishaginFunction(Int32.Parse(temp));

                constraints.Clear();
                temp = file.ReadLine();
                int numConstraints = Int32.Parse(temp);
                for (int i = 0; i < numConstraints; i++)
                {
                    temp = file.ReadLine();
                    string[] numbers = temp.Split(' ');
                    Constraint constraint = new Constraint();
                    constraint.A = Double.Parse(numbers[0]);
                    constraint.B = Double.Parse(numbers[1]);
                    constraint.F = new GrishaginFunction(Int32.Parse(numbers[2]));
                    constraints.Add(constraint);
                }

                ResetControls();

                temp = file.ReadLine();
                int numPoints = Int32.Parse(temp);

                PointF[] points = new PointF[numPoints];
                int[] indices = new int[numPoints];
                for (int i = 0; i < numPoints; i++)
                {
                    temp = file.ReadLine();
                    string[] numbers = temp.Split(' ');
                    points[i] = new PointF(Single.Parse(numbers[1], CultureInfo.InvariantCulture),
                        Single.Parse(numbers[2], CultureInfo.InvariantCulture));
                    indices[i] = Int32.Parse(numbers[3]);
                }

                int partSize = numPoints;
                if (dialog.ShowMode == ShowMode.Partial)
                    partSize = dialog.PartSize;
                if (partSize > numPoints)
                    partSize = numPoints;

                labelIterations.Text = OptimLab.Properties.Resources.LabelIterationsText + ": " + numPoints;

                int shownPoints = 0;
                while (shownPoints < numPoints)
                {
                    int currPartSize = Math.Min(partSize, numPoints - shownPoints);
                    for (int i = 0; i < currPartSize; i++)
                    {
                        Plot plot = Triangulator.PointsVertexArray(new PointF[] { points[shownPoints] });
                        plot.Information.Name = "TP" + shownPoints;
                        if (indices[shownPoints] == constraints.Count)
                            plot.Color = Color.Blue;
                        else
                            plot.Color = Color.Black;
                        plotBuilder.AddPlot(plot);
                        shownPoints++;
                    }
                    Application.DoEvents();
                    Thread.Sleep(400);
                }

                toolStripMenuItemSolve.Enabled = false;
                toolStripMenuItemMethod.Enabled = false;
                toolStripMenuItemSaveResults.Enabled = false;
            }
            dialog.Dispose();
        }

        private void toolStripMenuItemTask_Click(object sender, EventArgs e)
        {
            FormProblem dialog = new FormProblem();
            dialog.TargetFunctionIndex = targetFunction.Index;
            dialog.NumConstraints = constraints.Count;
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                targetFunction = new GrishaginFunction(dialog.TargetFunctionIndex);
                if (dialog.RegenerateConstraints || (dialog.NumConstraints != constraints.Count))
                {
                    constraints.Clear();
                    for (int i = 0; i < dialog.NumConstraints; i++)
                        GenerateConstraint();
                }

                ResetControls();

                toolStripMenuItemSolve.Enabled = true;
                toolStripMenuItemMethod.Enabled = true;
                toolStripMenuItemSaveResults.Enabled = false;
            }
            dialog.Dispose();
        }

        private void toolStripMenuItemMethodOptions_Click(object sender, EventArgs e)
        {
            FormMethodOptions dialog = new FormMethodOptions();
            dialog.SetMethodOptions(methodOptions);
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                dialog.GetMethodOptions(ref methodOptions);
                toolStripMenuItemSaveResults.Enabled = false;
            }
        }

        private void toolStripMenuItemSolve_Click(object sender, EventArgs e)
        {
            Problem problem = new Problem();
            problem.Left = new double[] { 0.0, 0.0 };
            problem.Right = new double[] { 1.0, 1.0 };

            problem.TargetFunction = delegate(double[] args)
            {
                return -targetFunction.CalculateFunction(args);
            };

            problem.Constraints = new List<FunctionDelegate>();
            if (constraints.Count >= 1)
            {
                problem.Constraints.Add(
                    delegate(double[] args)
                    {
                        return constraints[0].A * constraints[0].F.CalculateFunction(args) +
                            constraints[0].B;
                    }
                );
            }
            if (constraints.Count >= 2)
            {
                problem.Constraints.Add(
                    delegate(double[] args)
                    {
                        return constraints[1].A * constraints[1].F.CalculateFunction(args) +
                            constraints[1].B;
                    }
                );
            }
            if (constraints.Count >= 3)
            {
                problem.Constraints.Add(
                    delegate(double[] args)
                    {
                        return constraints[2].A * constraints[2].F.CalculateFunction(args) +
                            constraints[2].B;
                    }
                );
            }

            method = (Method)Activator.CreateInstance(methods[currentMethodName]);
            if (currentMethodName == "ModReductionMethod")
            {
                problem.TargetFunction = delegate(double[] args)
                {
                    return args[0] * args[0] - args[1] * args[1] -
                        Math.Cos(18.0 * args[0]) - Math.Cos(18.0 * args[1]);
                };

                plotBuilder.RemoveAllPlots();
                plotBuilder.RemoveAllConstraints();

                Plot plot = Triangulator.SurfaceLevelLines(
                    delegate(double x, double y)
                    {
                        return problem.TargetFunction(new double[] { x, y });
                    },
                    0.0, 1.0, 0.0, 1.0, 100, 100, 10);
                plot.Information.Name = "Target function";
                plot.Color = Color.Black;
                plotBuilder.AddPlot(plot);

                plotBuilder.ShowPicture = false;
            }
            else
            {
                ResetControls();
                plotBuilder.ShowPicture = true;
            }
            method.SetProblem(problem);
            method.SetOptions(methodOptions);
            method.IsOperationProperty = false;
            Cursor = Cursors.WaitCursor;
            method.Solve();
            Cursor = Cursors.Default;

            Experiment experiment = new Experiment();
            experiment.MethodName = currentMethodName;
            experiment.MethodVisibleName = method.GetMethodName();
            experiment.MethodOptions =
                ((Method)Activator.CreateInstance(methods[currentMethodName])).GetDefaultOptions();
            methodOptions.CopyTo(experiment.MethodOptions);
            experiments.Add(experiment);

            Result result = method.GetResult();
            labelIterations.Text = OptimLab.Properties.Resources.LabelIterationsText + ": " + result.Iterations;

            labelPoint.Text = OptimLab.Properties.Resources.LabelPointText + ": " +
                result.Point[0].ToString("F5", CultureInfo.InvariantCulture) + "; " +
                result.Point[1].ToString("F5", CultureInfo.InvariantCulture);
            labelValue.Text = OptimLab.Properties.Resources.LabelValueText + ": " +
                (-targetFunction.CalculateFunction(result.Point)).ToString("F5", CultureInfo.InvariantCulture);

            double errorInPoint = Math.Max(
                Math.Abs(targetFunction.MaximumPoint[0] - result.Point[0]),
                Math.Abs(targetFunction.MaximumPoint[1] - result.Point[1]));
            labelErrorInPoint.Text = OptimLab.Properties.Resources.LabelErrorInPointText + ": " +
                errorInPoint.ToString("F5", CultureInfo.InvariantCulture);

            double errorInValue = Math.Abs(
                (targetFunction.MaximumValue - targetFunction.CalculateFunction(result.Point)) /
                (targetFunction.MaximumValue - targetFunction.MinimumValue));
            labelErrorInValue.Text = OptimLab.Properties.Resources.LabelErrorInValueText + ": " +
                errorInValue.ToString("F5", CultureInfo.InvariantCulture);

            labelEvalsTarget.Text = OptimLab.Properties.Resources.LabelEvalsTargetText + ": " +
                result.TargetFunctionEvals;
            labelEvalsConst1.Text = OptimLab.Properties.Resources.LabelEvalsConst1Text + ": " +
                ((constraints.Count >= 1) ? result.ConstraintEvals[0] : 0);
            labelEvalsConst2.Text = OptimLab.Properties.Resources.LabelEvalsConst2Text + ": " +
                ((constraints.Count >= 2) ? result.ConstraintEvals[1] : 0);
            labelEvalsConst3.Text = OptimLab.Properties.Resources.LabelEvalsConst3Text + ": " +
                ((constraints.Count >= 3) ? result.ConstraintEvals[2] : 0);

            List<Trial> trials = method.GetTrials();
            List<PointF> goodPoints = new List<PointF>();
            List<PointF> badPoints = new List<PointF>();
            foreach (Trial trial in trials)
            {
                if (trial.Index == constraints.Count)
                    goodPoints.Add(new PointF((float)trial.Y[0], (float)trial.Y[1]));
                else if (trial.Index >= 0)
                    badPoints.Add(new PointF((float)trial.Y[0], (float)trial.Y[1]));
            }

            Plot plotGood = Triangulator.PointsVertexArray(goodPoints.ToArray());
            plotGood.Information.Name = "Good points";
            plotGood.Color = Color.Lime;
            if (plotBuilder.IsExistingPlot("Good points"))
                plotBuilder.RemovePlot("Good points");
            plotBuilder.AddPlot(plotGood);

            Plot plotBad = Triangulator.PointsVertexArray(badPoints.ToArray());
            plotBad.Information.Name = "Bad points";
            plotBad.Color = Color.Black;
            if (plotBuilder.IsExistingPlot("Bad points"))
                plotBuilder.RemovePlot("Bad points");
            plotBuilder.AddPlot(plotBad);

            toolStripMenuItemSaveResults.Enabled = true;
        }

        private void toolStripMenuItemOperationProperty_Click(object sender, EventArgs e)
        {
            List<int> indices = new List<int>();

            FormExperiments dialogExperiments = new FormExperiments();
            dialogExperiments.Initialize(experiments);
            if (dialogExperiments.ShowDialog() != DialogResult.OK)
            {
                dialogExperiments.Dispose();
                return;
            }

            indices = dialogExperiments.GetIndices();
            if (indices.Count == 0)
            {
                MessageBox.Show("Не выбрано ни одного эксперимента", "Информация", MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                return;
            }
            dialogExperiments.Dispose();

            FormOperationProperties dialogGraph = new FormOperationProperties();

            PlotBuilder graph = new PlotBuilder();
            graph.ShowPicture = false;
            graph.Width = 600;
            graph.Height = 432;
            graph.Location = new Point(10, 10);
            dialogGraph.Controls.Add(graph);

            for (int i = 0; i < indices.Count; i++)
            {
                Color color = StandardColors[i % StandardColors.Length];
                DrawOperationProperty(indices[i], color, graph);
                dialogGraph.AddLegendRecord(experiments[indices[i]], color);
            }

            Plot plotStub = Triangulator.PointsVertexArray(new PointF[] { new PointF(-5.0f, -8.0f) });
            plotStub.Information.Name = "Stub";
            plotStub.Color = Color.White;
            graph.AddPlot(plotStub);

            dialogGraph.ShowDialog();
            dialogGraph.Dispose();
        }

        private void toolStripMenuItemClear_Click(object sender, EventArgs e)
        {
            experiments.Clear();
        }

        private void FormMain_Resize(object sender, EventArgs e)
        {
            SetSizesAndLocations();
        }
    }
}