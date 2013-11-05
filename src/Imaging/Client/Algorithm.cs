using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Femore.Imaging.Client
{
    [Serializable]
    public sealed class Algorithm : ICollection<IImageProcess>, IEquatable<Algorithm>
    {
        public Algorithm()
        {
            _processes = new List<IImageProcess>();
        }


        public void Add( IImageProcess item )
        {
            _processes.Add( item );
        }

        public void Clear()
        {
            _processes.Clear();
        }

        public bool Contains( IImageProcess item )
        {
            return _processes.Contains( item );
        }

        public void CopyTo( IImageProcess[] array, int arrayIndex )
        {
            _processes.CopyTo( array, arrayIndex );
        }

        public int Count
        {
            get
            {
                return _processes.Count;
            }
        }

        public bool IsReadOnly
        {
            get
            {
                return false;
            }
        }

        public bool Remove( IImageProcess item )
        {
            return _processes.Remove( item );
        }

        public IEnumerator<IImageProcess> GetEnumerator()
        {
            return _processes.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public bool Equals( Algorithm other )
        {
            if( other == null )
            {
                return false;
            }

            if( Count != other.Count )
            {
                return false;
            }

            // TODO Implement
            for( int index = 0; index < Count; index++ )
            {
                IImageProcess thisProcess = this.ElementAt( index );
                IImageProcess otherProcess = other.ElementAt( index );

                if( thisProcess.Equals( otherProcess ) == false )
                {
                    return false;
                }
            }

            return true;
        }



        private List<IImageProcess> _processes;
    }
}
