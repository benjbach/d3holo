using UnityEngine;
using System.Collections;
 using System.Collections.Generic;

public class Scatterplot : MonoBehaviour {


	// DATA PARAMETERS
	int DATA_NUM = 100;
	int SIZE_X = 10;
	int SIZE_Y = 10;
	int SIZE_Z = 10;
		
	

	// VISUAL PARAMETERS
	int POINT_SIZE = 1;
	float POINT_OPACITY = 1f;
	float POINT_OPACITY_GHOST = .1f;
	float POINT_OPACITY_GHOST_2 = .2f;
	

	// Interaction states
	int _activeTimeSlice = 4;
	int _activeNodeSlice = 4;

	bool shiftDown = false;

	// Visual objects
	Selection cubeCells;

	void Start () 
	{
	
		 // Dummy data
		 List<DataObject> connections = getData();

		 cubeCells = d4.selectAll()
		 	.data(connections)
			.append("sphere")
				.attr("r", (d,i) => getScale(d))
				// .attr("r", 1)
				.attr("x", (d,i) => getXPos(d))
				.attr("y", (d,i) => getYPos(d))
				.attr("z", (d,i) => getZPos(d))
				// .style("fill", (d,i) => getColor(d))
				// .style("opacity",  (d,i) => getOpacity(d))
				;

	}
	
	// Update is called once per frame
	void Update () {

		// if (Input.GetKey("up")){
		// 	_activeTimeSlice--;
		// 	_activePlane = PLANE_Z;
		// } 
		// if (Input.GetKey("down")){
		// 	_activeTimeSlice++;
		// 	_activePlane = PLANE_Z;
		// } 
		// if (Input.GetKey("left")){
		// 	_activeNodeSlice--;
		// 	_activePlane = PLANE_X;
		// } 
		// if (Input.GetKey("right")){
		// 	_activeNodeSlice++;
		// 	_activePlane = PLANE_X;
		// } 

		// _activeTimeSlice = (_activeTimeSlice + TIME_NUM) % TIME_NUM;
		// _activeNodeSlice = (_activeNodeSlice + NODE_NUM) % NODE_NUM;


		// shiftDown = Input.GetKey(KeyCode.LeftShift);


        
		// cubeCells
		// 	.style("opacity",  (d,i) => getOpacity(d));

	}
	


	/////////////////////
	// VISUAL MAPPINGS //
	/////////////////////


	public float getXPos(DataObject d)
	{
		return SIZE_X * d.Float("dim1");
	}

	public float getYPos(DataObject d)
	{
		return SIZE_Y * d.Float("dim2");
	}

	public float getZPos(DataObject d)
	{
		return SIZE_Z * d.Float("dim3");
	}

	public float getScale(DataObject d)
	{
		return POINT_SIZE * d.Float("dim4") ;
	}

	// public float getOpacity(DataObject d)
	// {
	// 	if(shiftDown)
	// 	{
	// 		if(d.Int("timeId") == _activeTimeSlice
	// 		&& d.Int("sourceId") == _activeNodeSlice){
	// 			return 1f;			
	// 		}

	// 		if(d.Int("timeId") == _activeTimeSlice)
	// 			return CELL_OPACITY_GHOST_2;

	// 		if(d.Int("sourceId") == _activeNodeSlice)
	// 			return CELL_OPACITY_GHOST_2;
		
	// 		return 0f;
	// 	}
	// 	else
	// 	{
	// 		if(_activePlane == PLANE_Z && d.Int("timeId") == _activeTimeSlice)
	// 			return 1f;

	// 		if(_activePlane == PLANE_X && d.Int("sourceId") == _activeNodeSlice)
	// 			return 1f;
	
	// 		return CELL_OPACITY_GHOST;
	// 	}

	// }




	//////////////////////
	// DATA PREPERATION //
	//////////////////////

	List<DataObject> getData ()
	{
		List<DataObject> data = new List<DataObject>();
		DataObject obj;

		for( int i=0 ; i < DATA_NUM ; i++)
		{
			obj = new DataObject();
			obj.attributes["dim1"] = Random.value;
			obj.attributes["dim2"] = Random.value;
			obj.attributes["dim3"] = Random.value;
			obj.attributes["dim4"] = Random.value;
			
			data.Add(obj);

		}
		return data;
	} 

}
