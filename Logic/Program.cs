using System;
using System.Collections.Generic;
using System.Linq;

namespace Logic
{
    public class TestModel
    {
        public List<SubTestModel> SubTest { get; set; }
    }

    public class SubTestModel
    {
        public DocumentType Document { get; set; }
    }

    public enum DocumentType
    {
        Insurance = 1,
        MOT,
        VehicleRegistration
    }

    public enum ApiValidationStatus
    {
        Passed = 1,
        Failed
    }

    class Program
    {
        public enum Check
        {
            Truck = 1,
            Marketer
        }

        static DocumentType[] documentTypes = new DocumentType[] { DocumentType.Insurance, DocumentType.MOT};
        static DocumentType[] documentTypes2 = new DocumentType[] { DocumentType.MOT, DocumentType.VehicleRegistration, DocumentType.Insurance};
        static ApiValidationStatus[] documentTypes3 = new ApiValidationStatus[] { ApiValidationStatus.Passed, ApiValidationStatus.Passed, ApiValidationStatus.Passed, ApiValidationStatus.Failed};

        static void Main(string[] args)
        {
            /** Remove sample **/
            string founder = "Mahesh Chand is a founder of C# Corner!";
            // Remove last character from a string  
            string founderMinus1 = founder.Remove(founder.Length - 1, 1);
            Console.WriteLine(founderMinus1);
        }

        

        private static bool DocumentTypeCheck()
        {
            for (int i = 0; i < documentTypes2.Count(); i++)
            {
                if (!documentTypes.Contains(documentTypes2[i]))
                    return false;
            }
            return true;
        }

        private static bool DocumentCheck()
        {
            var docArray = documentTypes3;
            for (int i = 0; i < documentTypes3.Count(); i++)
            {
                if (docArray[i] == ApiValidationStatus.Failed)
                {
                    i = documentTypes3.Count();
                    return false;
                }
            }

            return true;
        }
    }
}
