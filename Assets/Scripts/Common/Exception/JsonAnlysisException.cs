using System;

namespace SUIFW
{
	public class JsonAnlysisException : Exception {
	    public JsonAnlysisException() : base(){}
	    public JsonAnlysisException(string exceptionMessage) : base(exceptionMessage){}
	}
}