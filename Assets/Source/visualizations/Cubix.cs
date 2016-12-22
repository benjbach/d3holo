using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Cubix : MonoBehaviour {


	// DATA PARAMETERS
	int NODE_NUM = 10;
	int TIME_NUM = 20;
		
	

	// VISUAL PARAMETERS
	float CELL_UNIT = 1f;
	float CELL_SCALE = .9f;
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

	float planeZ = 10f;

	// Visual objects
	Selection cubeCells;
	Selection cuttingPlane;

	List<GameObject> cuttingplaneCorners = new List<GameObject>(); 
	List<DataObject> intersectedObjects = new List<DataObject>();

	void Start () 
	{
	
		 // Dummy data
		 List<DataObject> connections = getData();

		 cubeCells = d4.selectAll()
		 	.data(connections)
			.append("cube")
				.attr("scale", (d,i) => getScale(d))
				.attr("x", (d,i) => getXPos(d))
				.attr("y", (d,i) => getYPos(d))
				.attr("z", (d,i) => getZPos(d))
				.style("fill", (d,i) => getColor(d))
				.style("opacity",  (d,i) => getOpacity(d))
				;

			// cuttingPlaneCorners.Add( makeDataObject(0,0,0) );
			// cuttingPlaneCorners.Add( makeDataObject(0,0,0) );
			// cuttingPlaneCorners.Add( makeDataObject(0,0,0) );
			// cuttingPlaneCorners.Add( makeDataObject(0,0,0) );

			// cuttingPlane = d4.selectAll()
			// 	.data(cuttingPlaneCorners)
			// 	.append("sphere")
			// 		.attr("r", CELL_UNIT*2)
			// 		.style("fill", new float[]{1, 0, 0})
			// 		;

			// Attach cutting 
			cuttingplaneCorners.Add(GameObject.Find("CuttingplaneCorner1"));
			cuttingplaneCorners.Add(GameObject.Find("CuttingplaneCorner2"));
			cuttingplaneCorners.Add(GameObject.Find("CuttingplaneCorner3"));
			cuttingplaneCorners.Add(GameObject.Find("CuttingplaneCorner4"));


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
		
		// if (Input.GetKey("left")){
		// 	planeZ--;
		// } 
		// if (Input.GetKey("right")){
		// 	planeZ++;
		// } 



		// cuttingPlaneCorners[0] = makeDataObject(10f, 10f, planeZ);
		// cuttingPlaneCorners[1] = makeDataObject(-10f, 10f, planeZ);
		// cuttingPlaneCorners[2] = makeDataObject(-10f, -10f, planeZ);
		// cuttingPlaneCorners[3] = makeDataObject(10f, -10f, planeZ);

		this.intersectedObjects = atelier.cuttingPlane(
			cuttingplaneCorners[0].transform.position,
			cuttingplaneCorners[1].transform.position,
			cuttingplaneCorners[2].transform.position,
			cubeCells, 
			CELL_UNIT);

		// cuttingPlane
		// 	.data(cuttingPlaneCorners);
		
		// cuttingPlane
		// 	.attr("x", (d,i)=> CELL_UNIT * d.Float("x"))
		// 	.attr("y", (d,i)=> CELL_UNIT * d.Float("y"))
		// 	.attr("z", (d,i)=> CELL_UNIT * d.Float("z"))
		// 	;

		cubeCells
			.style("opacity",  (d,i) => getOpacity(d));


	}
	


	/////////////////////
	// VISUAL MAPPINGS //
	/////////////////////


	public float getXPos(DataObject d)
	{
		return CELL_UNIT * d.Int("sourceId");
	}

	public float getYPos(DataObject d)
	{
		return CELL_UNIT * d.Int("targetId");
	}

	public float getZPos(DataObject d)
	{
		return CELL_UNIT * d.Int("timeId");
	}

	public float getScale(DataObject d)
	{
		return CELL_UNIT * d.Float("weight") * CELL_SCALE;
	}

	public float[] getColor(DataObject d)
	{
		float w = d.Float("weight");
		return new float[]{w, w, w};
	}

	public float getOpacity(DataObject d)
	{
		// if(shiftDown)
		// {
		// 	if(d.Int("timeId") == _activeTimeSlice
		// 	&& d.Int("sourceId") == _activeNodeSlice){
		// 		return 1f;			
		// 	}

		// 	if(d.Int("timeId") == _activeTimeSlice)
		// 		return CELL_OPACITY_GHOST_2;

		// 	if(d.Int("sourceId") == _activeNodeSlice)
		// 		return CELL_OPACITY_GHOST_2;
		
		// 	return 0f;
		// }
		// else
		// {
		// 	if(_activePlane == PLANE_Z && d.Int("timeId") == _activeTimeSlice)
		// 		return 1f;

		// 	if(_activePlane == PLANE_X && d.Int("sourceId") == _activeNodeSlice)
		// 		return 1f;
	
		// 	return CELL_OPACITY_GHOST;
		// }

		// print(">>>" + this.intersectedObjects.Count + ", " +  d);
		if(this.intersectedObjects.Contains(d))
			return CELL_OPACITY;
		
		return CELL_OPACITY_GHOST;

	}




	//////////////////////
	// DATA PREPERATION //
	//////////////////////

	List<DataObject> getData ()
	{
		List<DataObject> data = new List<DataObject>();
		DataObject obj;

		for( int i=0 ; i<NODE_NUM ; i++)
		{
			for( int j=0 ; j<NODE_NUM ; j++)
			{
				for( int k=0 ; k<TIME_NUM ; k++)
				{
					obj = new DataObject();
					obj.attributes["sourceId"] = i;
					obj.attributes["targetId"] = j;
					obj.attributes["timeId"] = k;
					obj.attributes["weight"] = Random.value;

					data.Add(obj);
				}
			}
		}
		return data;
	} 



	DataObject makeDataObject(float x, float y, float z){
		DataObject obj = new DataObject();
		obj.attributes["x"] = x;
		obj.attributes["y"] = y;
		obj.attributes["z"] = z;
		return obj;
	}

	Vector3 makeVector(DataObject obj){
		// print("obj.Float="+ obj.Float("x") + ", " + obj.attributes["x"]);
		return new Vector3(obj.Float("x"), obj.Float("y"), obj.Float("z"));
	}

		

}
