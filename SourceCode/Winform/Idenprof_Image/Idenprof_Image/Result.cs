using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Idenprof_Image
{
    public class DataResult_Loai04s
    {
        public string DataError { get; set; }
        public string DataErrorDescription { get; set; }
        public List<Data_Loai04> DataResults { get; set; }
        public static DataResult_Loai04s NewInstance()
        {
            return new DataResult_Loai04s();
        }
        public static DataResult_Loai04s NewInstance(string pDataError)
        {
            return new DataResult_Loai04s()
            {
                DataError = pDataError
            };
        }
        public static DataResult_Loai04s NewInstance(string pDataError, string pDataErrorDescription)
        {
            return new DataResult_Loai04s()
            {
                DataError = pDataError,
                DataErrorDescription = pDataErrorDescription
            };
        }
        public static DataResult_Loai04s NewInstance(string pDataError, string pDataErrorDescription, List<Data_Loai04> pDataResults)
        {
            return new DataResult_Loai04s()
            {
                DataError = pDataError,
                DataErrorDescription = pDataErrorDescription,
                DataResults = pDataResults
            };
        }
    }

    public class JsonResult
    {
        public string Name { get; set; }
        public float Point { get; set; }
    }
}
