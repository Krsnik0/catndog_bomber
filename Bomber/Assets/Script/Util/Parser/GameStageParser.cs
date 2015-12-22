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

        StageValueObject ret = new StageValueObject();

        XmlNode stageDataXML = stageXML.SelectSingleNode ("StageData");

        XmlNode mapSizeXML = stageDataXML.SelectSingleNode("MapSize");
        ret.mapSize = new IntegerPair(int.Parse(mapSizeXML.Attributes.GetNamedItem("x").InnerText),
                                     int.Parse(mapSizeXML.Attributes.GetNamedItem("y").InnerText));

        // Parse map & block prefab data

        XmlNode objLayerXML = stageDataXML.SelectSingleNode("ObjectLayer");
        string objLayerString = objLayerXML.InnerText;
        string[] objInLayer = objLayerString.Split(',');

        AbstractGameObjectValueObject objInPos;
		int i,j;

		ret.objLayer = new AbstractGameObjectValueObject[ret.mapSize.x][];
		usedObjects_ = new List<KeyValuePair<Type, string>> ();
		
		for( i = 0; i < ret.mapSize.x; ++ i )
		{
			ret.objLayer[i] = new AbstractGameObjectValueObject[ret.mapSize.y];
			for( j = 0; j < ret.mapSize.y; ++ j )
			{
				objInPos = BlockDataManager.getInstance().findBlockData( objInLayer[ i + j * ret.mapSize.x ] );
                if (objInPos == null)
                {
                    objInPos = CharacterDataManager.getInstance().findCharacterData(objInLayer[i + j * ret.mapSize.x]);
                }

                if (objInPos != null)
                {
                    if (!usedObjects_.Contains(objInPos.prefabData))
                    {
                        usedObjects_.Add(objInPos.prefabData);
                    }

                    ret.objLayer[i][j] = objInPos;
                }
			}
		}

		// Parse entry point & player prefab data
		XmlNode entryXML = stageDataXML.SelectSingleNode( "EntryPoint" );
		ret.entryPoint = new IntegerPair (int.Parse( entryXML.Attributes.GetNamedItem( "x" ).InnerText ),
                                     int.Parse(entryXML.Attributes.GetNamedItem("y").InnerText ) );

        XmlNode goalXML = stageDataXML.SelectSingleNode("Goal");
        ret.goal = new IntegerPair(int.Parse(goalXML.Attributes.GetNamedItem("x").InnerText),
                                     int.Parse(goalXML.Attributes.GetNamedItem("y").InnerText));

        ret.objLayer [ret.entryPoint.y] [ret.entryPoint.x] = CharacterDataManager.getInstance ().findCharacterData ("CHAR0000");
		usedObjects_.Add( CharacterDataManager.getInstance().findCharacterData( "CHAR0000" ).prefabData );

		// Parse bomb data
		XmlNodeList bombs = stageDataXML.SelectSingleNode ("Bombs").SelectNodes ("Bomb");
		AbstractBombValueObject allowedBomb;
		ret.allowedBombs = new AbstractBombValueObject[bombs.Count];

		for (i = 0; i < bombs.Count; ++ i) {
			allowedBomb = BombDataManager.getInstance().findBombData( bombs[i].Attributes.GetNamedItem( "code" ).InnerText );
			usedObjects_.Add( allowedBomb.prefabData );
			usedObjects_.Add( allowedBomb.iconPath );
			ret.allowedBombs[i] = allowedBomb;
		}

        /*XmlNodeList watchmen = stageDataXML.SelectSingleNode("Enemies").SelectNodes("Watchman");
        CharacterValueObject character;
        IntegerPair enemyPos = new IntegerPair( 0, 0 );
        for (i = 0; i < watchmen.Count; ++i)
        {
            character = CharacterDataManager.getInstance().findCharacterData(watchmen[i].Attributes.GetNamedItem("code").InnerText);
            usedObjects_.Add(character.prefabData);

            enemyPos.x = int.Parse(watchmen[i].Attributes.GetNamedItem("x").InnerText);
            enemyPos.y = int.Parse(watchmen[i].Attributes.GetNamedItem("y").InnerText);

            ret.objLayer[enemyPos.y][enemyPos.x] = character;
        }
        */

		// System blocks
		usedObjects_.Add( BlockDataManager.getInstance().findBlockData( "SYSBLOCK0000" ).prefabData );		// marker
		usedObjects_.Add( BlockDataManager.getInstance().findBlockData( "SYSBLOCK0001" ).prefabData );		// flame

		return ret;
	}
}
