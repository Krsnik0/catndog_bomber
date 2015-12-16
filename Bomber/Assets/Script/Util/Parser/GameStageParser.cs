using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;

public class GameStageParser {
	static public StageValueObject parseMap( string stage_, out List<KeyValuePair<System.Type, string>> usedObjects_ )
	{
		string stageXMLPath = "XML/Stage/" + stage_;
		
		XmlDocument stageXML = new XmlDocument ();
		stageXML.LoadXml (Resources.Load<TextAsset> (stageXMLPath).text);
		
		XmlNode stageDataXML = stageXML.SelectSingleNode ("StageData");


		// Parse map & block prefab data
		XmlNode objLayerXML = stageDataXML.SelectSingleNode ("ObjectLayer");
		
		string objLayerString = objLayerXML.InnerText;
		string[] objLayerRow = objLayerString.Split( '\n' );
		string[] objLayerCol;
		BlockValueObject blockVO;
		int i,j;

		StageValueObject ret = new StageValueObject ();

		ret.objLayer = new AbstractGameObjectValueObject[objLayerRow.Length][];
		usedObjects_ = new List<KeyValuePair<Type, string>> ();
		
		for( i = 0; i < objLayerRow.Length; ++ i )
		{
			objLayerCol = objLayerRow[i].Split( ',' );
			ret.objLayer[i] = new AbstractGameObjectValueObject[objLayerCol.Length];
			for( j = 0; j < objLayerCol.Length; ++ j )
			{
				blockVO = BlockDataManager.getInstance().findBlockData( objLayerCol[j] );
				if( blockVO != null )
				{
					if( !usedObjects_.Contains( blockVO.prefabData ) )
					{
						usedObjects_.Add( blockVO.prefabData );
					}

					ret.objLayer[i][j] = blockVO;
				}
			}
			//Debug.Log( objLayerCol[9] );
		}

		// Parse entry point & player prefab data
		XmlNode entryXML = stageDataXML.SelectSingleNode( "EntryPoint" );
		ret.entryPoint = new IntegerPair (int.Parse (entryXML.SelectSingleNode ("x").InnerText),
		                             int.Parse (entryXML.SelectSingleNode ("y").InnerText));

		ret.objLayer [ret.entryPoint.x] [ret.entryPoint.y] = CharacterDataManager.getInstance ().findCharacterData ("CHAR0000");
		usedObjects_.Add( CharacterDataManager.getInstance().findCharacterData( "CHAR0000" ).prefabData );

		// Parse bomb data
		XmlNodeList bombs = stageDataXML.SelectSingleNode ("Bombs").SelectNodes ("Code");
		AbstractBombValueObject allowedBomb;
		ret.allowedBombs = new AbstractBombValueObject[bombs.Count];

		for (i = 0; i < bombs.Count; ++ i) {
			allowedBomb = BombDataManager.getInstance().findBombData( bombs[i].InnerText );
			usedObjects_.Add( allowedBomb.prefabData );
			usedObjects_.Add( allowedBomb.iconPath );
			ret.allowedBombs[i] = allowedBomb;
		}

		// System blocks
		usedObjects_.Add( BlockDataManager.getInstance().findBlockData( "SYSBLOCK0000" ).prefabData );		// marker
		usedObjects_.Add( BlockDataManager.getInstance().findBlockData( "SYSBLOCK0001" ).prefabData );		// flame

		return ret;
	}
}
