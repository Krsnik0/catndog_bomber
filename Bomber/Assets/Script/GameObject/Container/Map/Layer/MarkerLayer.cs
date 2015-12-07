using UnityEngine;
using System.Collections;

public class MarkerLayer : AbstractLayer {

	private IntegerPair _selectedPosition;

	// Use this for initialization
	void Start () {
		initObject ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void markThrowableArea( IntegerPair position_, int radius_, Color color_ )
	{
		int i, j;
		int minX = Mathf.Max (position_.x - radius_, 0);
		int maxX = Mathf.Min (position_.x + radius_, layerSize.x - 1);
		int minY;
		int maxY;

		BlockValueObject markerBlock = BlockDataManager.getInstance ().findBlockData ("SYSBLOCK0000");
		AbstractGameObject existingMarker;
		MarkerBlock newMarker;
		for( i = minX; i <= maxX; ++ i )
		{
			minY = Mathf.Max( position_.y - (radius_ - Mathf.Abs( position_.x - i )), 0 );
			maxY = Mathf.Min( position_.y + (radius_ - Mathf.Abs( position_.x - i )), layerSize.y - 1 );
			for( j = minY; j <= maxY; ++ j )
			{
				if( GameMap.getInstance().isThrowable( i, j ) )
				{
					existingMarker = getObjectAt( i, j );
					
					if( existingMarker != null )
					{
						existingMarker.destroyObject();
					}
					
					newMarker = GameObjectFactory.getInstance().generateObject( markerBlock.prefabData.Value ).GetComponent<MarkerBlock>();
					newMarker.positionIndexPair = new IntegerPair( i, j );
					newMarker.gameObject.GetComponentInChildren<Renderer>().material.color = color_;
					newMarker.isTouchable = true;
					addObject( newMarker );
				}
			}
		}
	}

	public void markExplosionArea( IntegerPair position_, bool[][] area_, IntegerPair positionInArea_, Color color_ )
	{
		_selectedPosition = position_.clone ();
		int i, j;

		//Debug.Log (position_);
		IntegerPair leftTop = new IntegerPair( 
		                                      Mathf.Max( position_.x - positionInArea_.x, 0 ),
		                                      Mathf.Max( position_.y - positionInArea_.y, 0 ) );
		IntegerPair rightBottom = new IntegerPair( 
		                                          Mathf.Min( position_.x - positionInArea_.x + area_.Length, layerSize.x - 1 ),
		                                          Mathf.Min( position_.y - positionInArea_.y + area_[0].Length, layerSize.y - 1 ) );

		BlockValueObject markerBlock = BlockDataManager.getInstance ().findBlockData ("SYSBLOCK0000");
		AbstractGameObject existingMarker;
		MarkerBlock newMarker;
		Vector3 pos;

		//Debug.Log (leftTop);
		//Debug.Log (rightBottom);

		for( i = leftTop.x; i < rightBottom.x; ++ i )
		{
			for( j = leftTop.y; j < rightBottom.y; ++ j )
			{
				if( area_[ i - leftTop.x ][ j - leftTop.y ] )
				{
					existingMarker = getObjectAt( i, j );
					if( existingMarker != null )
					{
						existingMarker.destroyObject();
					}

					newMarker = GameObjectFactory.getInstance().generateObject( markerBlock.prefabData.Value ).GetComponent<MarkerBlock>();
					pos = PositionCalcUtil.mapIndexToVector3( new IntegerPair( i, j ) );
					pos.z = -9f;			// always on top
					newMarker.transform.position = pos;
					newMarker.gameObject.GetComponentInChildren<Renderer>().material.color = color_;
					newMarker.isTouchable = false;
					addObject( newMarker );
				}
			}
		}
	}

	public override void removeAll (bool destroyFlag)
	{
		base.removeAll (destroyFlag);

		_selectedPosition = null;
	}

	public bool isSelectedBlock( IntegerPair position_ )
	{
		return _selectedPosition != null && position_.ToString () == _selectedPosition.ToString ();
	}
}