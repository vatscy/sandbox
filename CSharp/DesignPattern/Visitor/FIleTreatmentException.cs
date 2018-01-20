using System;

namespace Vatscy.DesignPattern.Visitor
{
    public class FileTreatmentException : Exception
    {
        public FileTreatmentException() { }

        public FileTreatmentException(string message) : base(message) { }
    }
}