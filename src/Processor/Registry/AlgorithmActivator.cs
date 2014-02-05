﻿using DIPS.Processor.Client;
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

            if( RegistryCache.Cache
                .HasCachedAlgorithm( definition.AlgorithmName ) == false )
            {
                return false;
            }

            // We need a parameterless constructor.
            Type type = RegistryCache.Cache.ResolveType( definition );
            if( type.GetConstructor( Type.EmptyTypes ) == null )
            {
                return false;
            }

            return true;
        }

        public AlgorithmPlugin Activate( AlgorithmDefinition definition )
        {
            if( CanActivate( definition ) == false )
            {
                throw new ArgumentException( "Cannot activate provided definition." );
            }

            Type type = RegistryCache.Cache.ResolveType( definition );
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
    }
}