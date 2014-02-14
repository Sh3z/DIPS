using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DIPS.Unity
{
    public static class ContainerContainsExtensions
    {
        public static bool Contains<T>( this IUnityContainer container )
        {
            if( container == null )
            {
                throw new NullReferenceException( "Null Unity container" );
            }

            T item = container.Resolve<T>();
            return item != null;
        }
    }
}
