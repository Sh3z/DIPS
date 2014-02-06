using DIPS.Processor.Client;
using DIPS.Processor.Plugin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DIPS.Processor.Registry
{
    /// <summary>
    /// Represents the object capable of reconstructing <see cref="AlgorithmPlugin"/>s
    /// bound by the registry, using <see cref="AlgorithmDefinition"/>s.
    /// </summary>
    public class AlgorithmActivator
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AlgorithmActivator"/>
        /// class.
        /// </summary>
        /// <param name="registrar">The <see cref="IAlgorithmRegistrar"/> containing
        /// the information about the known algorithms.</param>
        /// <exception cref="ArgumentNullException">registrar is null.</exception>
        public AlgorithmActivator( IAlgorithmRegistrar registrar )
        {
            if( registrar == null )
            {
                throw new ArgumentNullException( "registrar" );
            }

            _registrar = registrar;
        }


        /// <summary>
        /// Determines whether the <see cref="AlgorithmDefinition"/> provided can
        /// be activated into an object.
        /// </summary>
        /// <param name="definition">The <see cref="AlgorithmDefinition"/> to attempt
        /// to activate.</param>
        /// <returns><c>true</c> if the algorithm represented by the definition can be
        /// activated; otherwise, <c>false</c>.</returns>
        public bool CanActivate( AlgorithmDefinition definition )
        {
            if( definition == null )
            {
                return false;
            }

            if( _registrar.KnowsAlgorithm( definition.AlgorithmName ) == false )
            {
                return false;
            }

            // We need a parameterless constructor.
            Type type = _registrar.FetchType( definition.AlgorithmName );
            if( type.GetConstructor( Type.EmptyTypes ) == null )
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Actives the algorithm provided in the definition.
        /// </summary>
        /// <param name="definition">The <see cref="AlgorithmDefinition"/>
        /// to restore back into an object.</param>
        /// <returns>The <see cref="AlgorithmPlugin"/> represented by the
        /// <see cref="AlgorithmDefinition"/>.</returns>
        /// <exception cref="ArgumentException">the provided definition cannot
        /// be converted back into an object.</exception>
        public AlgorithmPlugin Activate( AlgorithmDefinition definition )
        {
            if( CanActivate( definition ) == false )
            {
                throw new ArgumentException( "Cannot activate provided definition." );
            }

            Type type = _registrar.FetchType( definition.AlgorithmName );
            AlgorithmPlugin plugin = Activator.CreateInstance( type ) as AlgorithmPlugin;

            // Use Linq to quickly grab all attributed properties.
            var attributedProperties =
                from p in type.GetProperties()
                let attr = p.GetCustomAttributes( false ).OfType<PluginVariableAttribute>().FirstOrDefault()
                where attr != null
                select new
                {
                    Property = p,
                    DefinedName = attr.VariableIdentifier
                };
            foreach( var attributedProperty in attributedProperties )
            {
                Property p = definition.Properties.FirstOrDefault( x => x.Name == attributedProperty.DefinedName );
                if( p != null )
                {
                    if( attributedProperty.Property.PropertyType == p.Type )
                    {
                        attributedProperty.Property.SetValue( plugin, p.Value );
                    }
                }
            }

            return plugin;
        }


        /// <summary>
        /// Contains the registrar encapsulating the known algorithms.
        /// </summary>
        private IAlgorithmRegistrar _registrar;
    }
}
