using System;
using System.Collections;
using System.Collections.Generic;

public class DataObject{
	
	public string name;
	public Dictionary<string, object> attributes = new Dictionary<string, object>();

	public DataObject(){}


	public int Int (string attributeName)
	{
		int val;
		try{
			val = (int) attributes[attributeName];
		}
		catch(System.Exception ex) {
			val = 0;
		}  		
		return val;
	}

	public float Float (string attributeName)
	{
		float val;
		try{
			val = (float) attributes[attributeName];
		}
		catch(System.Exception ex) {
			val = 0;
		}  		 
		return val;
	}

	public string String (string attributeName)
	{
		string val;
		try{
			val = (string) attributes[attributeName];
		}
		catch(System.Exception ex) {
			val = "";
		}  		
		return val;
	}

}
