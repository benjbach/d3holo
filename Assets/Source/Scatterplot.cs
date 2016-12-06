using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Scatterplot : MonoBehaviour 
{

	public static float POINT_SIZE = 2f;
	public static float VISUALIZATION_SIZE = 50f;

	public static bool ANIMATE = true;
	public static bool COLOR = true;
	public static float OPACITY = .9f;

	// STATES

	// public static bool ANIMATE = false;

	public static float minGDP; 
	public static float maxGDP; 
	public static float minMortality; 
	public static float maxMortality; 
	public static float minFertility; 
	public static float maxFertility; 
	public static float minPopulation; 
	public static float maxPopulation; 

	public static string yearMin = "1960";
	public static string yearMax = "2010";
	
	public static string year = "2000";
	public static int animationCounter = 0;



	// VISUAL ELEMENTS
	public static Selection dataPoints;

	// Use this for initialization
	void Start () {
	
		// load data: 	
		// d4.loadCSV("//users/~bbach/data/gapminder/GDPpercapitaconstant2000US.csv");
		List<Dictionary<string, object>> data = d4.loadCSV("GDPpercapitaconstant2000US");


		// Create data objects (countries)
		List<DataObject> countries = new List<DataObject>();
		
		Country c;
        for(var i=0; i < data.Count; i++) 
		{
			c = new Country();
			c.name = data[i]["Country"].ToString();
			countries.Add(c);
		}
		
		// Load GDP
		float[] res = addAttribute(countries, data, "gdp");
		minGDP = res[0];
		maxGDP = res[1];
		

		// Load child mortality
		res = addAttribute(countries, d4.loadCSV("under5mortality"), "childMortality");
		minMortality = res[0];
		maxMortality = res[1];


		// Load fertility
		res = addAttribute(countries, d4.loadCSV("total_fertility"), "fertility");
		minFertility = res[0];
		maxFertility = res[1];

		// Load population
		res = addAttribute(countries, d4.loadCSV("population"), "population");
		minPopulation = res[0];
		maxPopulation = res[1];


		// // CREATE DATA POINTS
		dataPoints = d4.selectAll()
			.data(countries)
			.append("sphere")
				.attr("r", (d,i) => Mathf.Sqrt(d4.map( minPopulation, maxPopulation, .2f, POINT_SIZE, getFloat(d, "population", year))))
				.attr("x", (d,i) => d4.map( minGDP, maxGDP, 0f, VISUALIZATION_SIZE, getFloat(d, "gdp", year)))
				.attr("y", (d,i) => d4.map( minMortality, maxMortality, 0f, VISUALIZATION_SIZE, getFloat(d, "childMortality", year)))
				.attr("z", (d,i) => d4.map( minFertility, maxFertility, 0f, VISUALIZATION_SIZE, getFloat(d, "fertility", year)))
				.style("opacity", OPACITY);
		
		if (COLOR){
			dataPoints
				.style("fill", (d,i) => new float[]{ Random.value, Random.value, Random.value} );
		}

	}
	
	// Update is called once per frame
	
	void Update () {

		if(!ANIMATE)
			return;	

		animationCounter += 1;

		if(animationCounter == 4){
			animationCounter = 0;
			int intYear = IntParseFast(year) + 1;
			year = intYear + "";

			dataPoints
				.attr("r", (d,i) => Mathf.Sqrt(d4.map( minPopulation, maxPopulation, .2f, POINT_SIZE, getFloat(d, "population", year))))
				.attr("x", (d,i) => d4.map( minGDP, maxGDP, 0f, VISUALIZATION_SIZE, getFloat(d, "gdp", year)))
				.attr("y", (d,i) => d4.map( minMortality, maxMortality, 0f, VISUALIZATION_SIZE, getFloat(d, "childMortality", year)))
				.attr("z", (d,i) => d4.map( minFertility, maxFertility, 0f, VISUALIZATION_SIZE, getFloat(d, "fertility", year)));
			if(year == yearMax){
				year = yearMin;
			}
		}

	}


	float[] addAttribute(List<DataObject> data, List<Dictionary<string, object>> newData, string attributeName)
	{
	    Dictionary<string, float> attr;
		Country c;
		string s;
		List<float> values = new List<float>();
		for(var i=0; i < newData.Count; i++)
		{
			attr = new Dictionary <string, float>();
			foreach(KeyValuePair<string, object> field in newData[i])
			{
				if(field.Key == "Country")
					continue;

				s = field.Value.ToString(); 
				if(s.Length == 0)
				{
					attr[field.Key] = -1f;					
				}else{
					attr[field.Key] = float.Parse(s);
				}
				values.Add((float) attr[field.Key]);

			}
			c = (Country) data[i];
			c.attributes[attributeName] = attr;
		}

		float[] res = new float[2];
		res[0] = d4.min(values);
		res[1] = d4.max(values);
		Debug.Log("Min, max: " + res[0] +", "+ res[1]);
		return res;
	}






	/////////////
	// HELPERS //
	/////////////

	float getFloat(DataObject d, string attr, string year)
	{
		
		float val;
		try{
			val = (float) ((Dictionary<string, float>) ((Country)d).attributes[attr])[year];
		}
		catch(System.Exception e) {
			val = 0;
		}  		
		// sprint(val);
		return val;
		// return 108978f * Random.value ;
	}


	float[] randomColor(){
		float[] c = new float[3];
		c[0] = Random.value;
		c[1] = Random.value;
		c[2] = Random.value;
		return c;
	}

	int IntParseFast( string v)
	{
		int result = 0;
		for (var i = 0; i < v.Length; i++)
		{
			char letter = v[i];
			result = (int) (10 * result + char.GetNumericValue(letter));
		}
		return result;
	}

}
