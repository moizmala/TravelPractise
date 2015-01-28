using System;
using System.Linq;

using CMS.ExtendedControls;
using CMS.Helpers;
using CMS.UIControls;
using CMS.MacroEngine;

public partial class CMSModules_System_Macros_Benchmark : CMSDebugPage
{
    #region "Nested classes - Benchmark"

    /// <summary>
    /// Provides support for benchmarking the code
    /// </summary>
    private class Benchmark<TResult>
    {
        #region "Variables"

        private int mIterations = 1000;

        #endregion


        #region "Properties"

        /// <summary>
        /// Number of iterations to run. Default 1000
        /// </summary>
        public int Iterations
        {
            get
            {
                return mIterations;
            }
            set
            {
                mIterations = value;
            }
        }


        /// <summary>
        /// Function to execute within each iteration
        /// </summary>
        public Func<Benchmark<TResult>, TResult> ExecuteFunc
        {
            get;
            set;
        }


        /// <summary>
        /// Results of the benchmarked function
        /// </summary>
        public TResult Result
        {
            get;
            set;
        }


        /// <summary>
        /// Results of the benchmark
        /// </summary>
        public BenchmarkResults Results
        {
            get;
            set;
        }


        /// <summary>
        /// Performs the setup of the benchmark
        /// </summary>
        public Action<Benchmark<TResult>> SetupFunc
        {
            get;
            set;
        }
        
        
        /// <summary>
        /// Performs the tear down of the benchmark
        /// </summary>
        public Action<Benchmark<TResult>> TearDownFunc
        {
            get;
            set;
        }

        #endregion


        #region "Methods"

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="executeFunc">Function to execute within each iteration</param>
        public Benchmark(Func<Benchmark<TResult>, TResult> executeFunc)
        {
            ExecuteFunc = executeFunc;
        }


        /// <summary>
        /// Runs the benchmark
        /// </summary>
        public void Run()
        {
            Result = default(TResult);

            // Setup
            if (SetupFunc != null)
            {
                SetupFunc(this);
            }

            int runs = 0;

            var startTime = DateTime.Now;

            double minRunSeconds = double.MaxValue;
            double maxRunSeconds = 0;

            double totalRunSeconds = 0;

            // Run the benchmark
            for (int i = 0; i < Iterations; i++)
            {
                var runStart = DateTime.Now;

                // Execute the run
                Result = ExecuteFunc(this);

                runs++;

                // Count the run time
                var runEnd = DateTime.Now;
                var runTime = runEnd - runStart;
                var runSeconds = runTime.TotalSeconds;

                if (runSeconds < minRunSeconds)
                {
                    minRunSeconds = runSeconds;
                }
                if (runSeconds > maxRunSeconds)
                {
                    maxRunSeconds = runSeconds;
                }

                totalRunSeconds += runSeconds;
            }

            var endTime = DateTime.Now;

            // Calculate the results
            if (Math.Abs(minRunSeconds - double.MaxValue) < Math.E)
            {
                minRunSeconds = 0;
            }

            var totalTime = endTime - startTime;

            var totalSeconds = totalTime.TotalSeconds;
            var secondsPerRun = totalSeconds / runs;

            // Set up the results
            Results = new BenchmarkResults
            {
                Runs = runs,
                TotalSeconds = totalSeconds,
                SecondsPerRun = secondsPerRun,
                MinRunSeconds = minRunSeconds,
                MaxRunSeconds = maxRunSeconds,
                TotalRunSeconds = totalRunSeconds
            };

            // Tear down
            if (TearDownFunc != null)
            {
                TearDownFunc(this);
            }
        }

        #endregion
    }


    /// <summary>
    /// Represents the results of the benchmark
    /// </summary>
    private class BenchmarkResults
    {
        #region "Properties"

        /// <summary>
        /// Total seconds for running all iterations
        /// </summary>
        public double TotalRunSeconds
        {
            get;
            set;
        }


        /// <summary>
        /// Maximum number of seconds per single run
        /// </summary>
        public double MaxRunSeconds
        {
            get;
            set;
        }


        /// <summary>
        /// Minimum seconds per single run
        /// </summary>
        public double MinRunSeconds
        {
            get;
            set;
        }


        /// <summary>
        /// Average seconds per single run
        /// </summary>
        public double SecondsPerRun
        {
            get;
            set;
        }


        /// <summary>
        /// Total number of seconds
        /// </summary>
        public double TotalSeconds
        {
            get;
            set;
        }


        /// <summary>
        /// Number of benchmark runs
        /// </summary>
        public int Runs
        {
            get;
            set;
        }

        #endregion


        #region "Methods"

        /// <summary>
        /// Returns the results as string representation
        /// </summary>
        public override string ToString()
        {
            var results = String.Format(
@"Total runs: {0}
Total benchmark time: {1:f5}s
Total run time: {5:f5}s

Average time per run: {2:f5}s
Min run time: {3:f5}s
Max run time: {4:f5}s
"
                , Runs
                , TotalSeconds
                , SecondsPerRun
                , MinRunSeconds
                , MaxRunSeconds
                , TotalRunSeconds
            );

            return results;
        }

        #endregion
    }

    #endregion


    protected void Page_Load(object sender, EventArgs e)
    {
        editorElem.Editor.Language = LanguageEnum.HTMLMixed;
    }


    protected void btnRun_Click(object sender, EventArgs e)
    {
        // Get the number of iterations
        int iterations = ValidationHelper.GetInteger(txtIterations.Text, 0);
        if (iterations <= 0)
        {
            ShowError(GetString("macros.benchmark.invaliditerations"));
            return;
        }

        // Prepare the benchmark
        var benchmark = new Benchmark<string>(Execute)
        {
            SetupFunc = Setup,
            Iterations = iterations,
            TearDownFunc = TearDown
        };

        // Run the benchmark
        benchmark.Run();

        // Append expression to the results
        var text = editorElem.Text;
        var results = benchmark.Results.ToString();

        results += String.Format(
@"
Evaluated text: 
---------------
{0}
", 
            text
        );

        // Update UI
        editorElem.Text = text;
        txtOutput.Text = benchmark.Result;
        txtResults.Text = results;
    }
    

    /// <summary>
    /// Sets up the benchmark
    /// </summary>
    /// <param name="benchmark">Benchmark</param>
    private void Setup(Benchmark<string> benchmark)
    {
        // No action by default
    }


    /// <summary>
    /// Executes the benchmark run
    /// </summary>
    /// <param name="benchmark">Benchmark</param>
    private string Execute(Benchmark<string> benchmark)
    {
        return MacroResolver.Resolve(editorElem.Text);
    }


    /// <summary>
    /// Cleans up after the benchmark
    /// </summary>
    /// <param name="benchmark">Benchmark</param>
    private void TearDown(Benchmark<string> benchmark)
    {
        // No action by default
    }
}
