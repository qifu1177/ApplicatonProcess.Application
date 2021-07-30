using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicatonProcess.Data
{
    public abstract class Singleton<T> where T: class
    {
        private static T _instance;
        public static T Instance
        {
            get
            {
                if (_instance == null)
                {
                    Type type = typeof(T);
                    _instance = (T)Activator.CreateInstance(type);
                }
                return _instance;
            }
        }

    }
}
