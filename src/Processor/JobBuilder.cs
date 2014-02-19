using DIPS.Processor.Client;
using DIPS.Processor.Client.JobDeployment;
using DIPS.Processor.Persistence;
using DIPS.Processor.Plugin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DIPS.Processor
{
    /// <summary>
    /// Represents the object used to construct <see cref="Job"/> objects.
    /// </summary>
    public class JobBuilder
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="IPluginFactory"/>
        /// class.
        /// </summary>
        /// <param name="factory">The <see cref="IPluginFactory"/> to use
        /// to resolve algorithms</param>
        /// <exception cref="ArgumentNullException">factory is null</exception>
        public JobBuilder( IPluginFactory factory )
        {
            if( factory == null )
            {
                throw new ArgumentNullException( "factory" );
            }

            _factory = factory;
            _inputs = new List<JobInput>();
            _algorithms = new List<AlgorithmDefinition>();
        }


        /// <summary>
        /// Gets the number of algorithms supplied to this <see cref="JobBuilder"/>.
        /// </summary>
        public int AlgorithmCount
        {
            get
            {
                return _algorithms.Count;
            }
        }

        /// <summary>
        /// Gets the number of inputs supplied to this <see cref="JobBuilder"/>.
        /// </summary>
        public int InputCount
        {
            get
            {
                return _inputs.Count;
            }
        }

        /// <summary>
        /// Gets the constructed <see cref="Job"/> object.
        /// </summary>
        public Job Job
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets or sets the <see cref="IJobPersister"/> the <see cref="Job"/>
        /// should use.
        /// </summary>
        public IJobPersister Persister
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the unique identifier of the job.
        /// </summary>
        public Guid JobID
        {
            get;
            set;
        }


        /// <summary>
        /// Resets the status of this <see cref="JobBuilder"/> to perform another
        /// build.
        /// </summary>
        public void Clear()
        {
            _inputs.Clear();
            _algorithms.Clear();
        }

        /// <summary>
        /// Adds an algorithm to this <see cref="JobBuilder"/>.
        /// </summary>
        /// <param name="definition">The <see cref="AlgorithmDefinition"/>
        /// to append.</param>
        public void AppendAlgorithm( AlgorithmDefinition definition )
        {
            if( definition != null )
            {
                _algorithms.Add( definition );
            }
        }

        /// <summary>
        /// Adds an input to this <see cref="JobBuilder"/>.
        /// </summary>
        /// <param name="input">The <see cref="JobInput"/> to append.</param>
        public void AppendInput( JobInput input )
        {
            if( input != null )
            {
                _inputs.Add( input );
            }
        }

        /// <summary>
        /// Applies the provided <see cref="IJobDefinition"/> to this
        /// <see cref="JobBuilder"/>.
        /// </summary>
        /// <param name="definition">The definition of the job to run.</param>
        public void ApplyDefinition( IJobDefinition definition )
        {
            if( definition != null )
            {
                _massAddAlgorithms( definition.GetAlgorithms() );
                _massAddInputs( definition.GetInputs() );
            }
        }

        /// <summary>
        /// Attemps to construct a <see cref="Job"/> using the information provided
        /// to this <see cref="JobBuilder"/>.
        /// </summary>
        /// <returns><c>true</c> if this builder represents a valid job and has been
        /// constructed correctly; <c>false</c> otherwise.</returns>
        public bool Build()
        {
            Job = null;
            if( Persister == null )
            {
                return false;
            }

            ICollection<PipelineEntry> plugins = _createPlugins();
            if( plugins.Any() == false )
            {
                return false;
            }

            if( plugins.Count != _algorithms.Count )
            {
                return false;
            }

            JobDefinition d = _createJobDefinition( plugins );
            Job = new Job( d );
            return true;
        }


        /// <summary>
        /// Creates a JobDefinition using the information within this builder
        /// </summary>
        /// <param name="plugins">The set of plugins to supply</param>
        /// <returns>A JobDefinition for use by a Job</returns>
        private JobDefinition _createJobDefinition( ICollection<PipelineEntry> plugins )
        {
            JobDefinition d = new JobDefinition( JobID, plugins, Persister );
            foreach( JobInput input in _inputs )
            {
                d.Inputs.Add( input );
            }

            return d;
        }

        /// <summary>
        /// Creates the set of plugins the job will use
        /// </summary>
        /// <returns>A collection of algorithm plugin objects representing
        /// the provided definitions</returns>
        private ICollection<PipelineEntry> _createPlugins()
        {
            ICollection<PipelineEntry> plugins = new List<PipelineEntry>();
            foreach( AlgorithmDefinition algorithm in _algorithms )
            {
                AlgorithmPlugin p = _factory.Manufacture( algorithm );
                if( p != null )
                {
                    PipelineEntry e = new PipelineEntry( p );
                    e.ProcessInput = algorithm.ParameterObject;
                    plugins.Add( e );
                }
            }

            return plugins;
        }


        /// <summary>
        /// Adds all the provided algorithms.
        /// </summary>
        /// <param name="algorithms">The algorithms to add.</param>
        private void _massAddAlgorithms( IEnumerable<AlgorithmDefinition> algorithms )
        {
            foreach( AlgorithmDefinition algorithm in algorithms )
            {
                AppendAlgorithm( algorithm );
            }
        }

        /// <summary>
        /// Adds all the provided inputs.
        /// </summary>
        /// <param name="inputs">The inputs to add.</param>
        private void _massAddInputs( IEnumerable<JobInput> inputs )
        {
            foreach( JobInput input in inputs )
            {
                AppendInput( input );
            }
        }


        /// <summary>
        /// Contains the factory used to resolve the plugins.
        /// </summary>
        private IPluginFactory _factory;

        /// <summary>
        /// Contains the set of inputs.
        /// </summary>
        private ICollection<JobInput> _inputs;

        /// <summary>
        /// Contains the set of algorithms.
        /// </summary>
        private ICollection<AlgorithmDefinition> _algorithms;
    }
}
