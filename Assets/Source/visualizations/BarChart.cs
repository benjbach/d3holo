using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BarChart : MonoBehaviour {

	// DATA PARAMETERS
	int DIM_1 = 20;
	int DIM_2 = 20;
	int HEIGHT_MAX = 5;

	// VISUAL PARAMETERS
	int CELL_UNIT = 1;
	float CELL_SCALE = .95f;
	float CELL_OPACITY = 1f;
	float CELL_OPACITY_GHOST = .1f;
	float CELL_OPACITY_GHOST_2 = .2f;


	// Interaction states
	int _activeTimeSlice = 4;
	int _activeNodeSlice = 4;
	int PLANE_Z = 0;
	int PLANE_X = 1;
	int PLANE_Y = 2;
	int _activePlane = 0;
	bool shiftDown = false;

	// Visual objects
	Selection cubeCells;


	// Use this for initialization
	void Start () {

		// Dummy data
		List<DataObject> connections = getData();

		cubeCells = d4.selectAll()
		 	.data(connections)
			.append("cube")
				.attr("x", (d,i) => getXPos(d))
				.attr("y", (d,i) => getHeight(d) /2)
				.attr("z", (d,i) => getZPos(d))
				.attr("scale", CELL_SCALE)
				.attr("height", (d,i) => getHeight(d))
				// .style("fill", (d,i) => getColor(d))
				.style("opacity",  (d,i) => getOpacity(d))
				;


	}
	
	// Update is called once per frame
	void Update () {
		
		if (Input.GetKey("up")){
			_activeTimeSlice--;
			_activePlane = PLANE_Z;
		} 
		if (Input.GetKey("down")){
			_activeTimeSlice++;
			_activePlane = PLANE_Z;
		} 
		if (Input.GetKey("left")){
			_activeNodeSlice--;
			_activePlane = PLANE_X;
		} 
		if (Input.GetKey("right")){
			_activeNodeSlice++;
			_activePlane = PLANE_X;
		} 


		_activeTimeSlice = (_activeTimeSlice + DIM_1) % DIM_1;
		_activeNodeSlice = (_activeNodeSlice + DIM_2) % DIM_2;


		shiftDown = Input.GetKey(KeyCode.LeftShift);

		cubeCells
			.style("opacity",  (d,i) => getOpacity(d));
	}


	/////////////////////
	// VISUAL MAPPINGS //
	/////////////////////


	public float getXPos(DataObject d)
	{
		return CELL_UNIT * d.Int("dim1");
	}

	public float getZPos(DataObject d)
	{
		return CELL_UNIT * d.Int("dim2");
	}

	public float getHeight(DataObject d)
	{
		return CELL_UNIT * d.Float("dim3") * HEIGHT_MAX;
	}


	public float getOpacity(DataObject d)
	{
		if(shiftDown)
		{
			if(d.Int("dim1") == _activeTimeSlice
			&& d.Int("dim2") == _activeNodeSlice){
				return 1f;			
			}

			if(d.Int("dim1") == _activeTimeSlice)
				return CELL_OPACITY_GHOST_2;

			if(d.Int("dim2") == _activeNodeSlice)
				return CELL_OPACITY_GHOST_2;
		
			return 0f;
		}
		else
		{
			if(_activePlane == PLANE_Z && d.Int("dim1") == _activeTimeSlice)
				return 1f;

			if(_activePlane == PLANE_X && d.Int("dim2") == _activeNodeSlice)
				return 1f;
	
			return CELL_OPACITY_GHOST;
		}

	}

	//////////////////////
	// DATA PREPERATION //
	//////////////////////

	List<DataObject> getData ()
	{
		List<DataObject> data = new List<DataObject>();
		DataObject obj;

		for( int i=0 ; i < DIM_1 ; i++)
		{
			for( int j=0 ; j< DIM_2 ; j++)
			{
				obj = new DataObject();
				obj.attributes["dim1"] = i;
				obj.attributes["dim2"] = j;
				obj.attributes["dim3"] = Random.value;

				data.Add(obj);
			}
		}
		return data;
	} 

}
