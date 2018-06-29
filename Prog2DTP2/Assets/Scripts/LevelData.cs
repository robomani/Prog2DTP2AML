using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/Level", fileName = "new Level", order = 1)]
public class LevelData : ScriptableObject
{
    [SerializeField] private TileColumn[] m_Tiles;

    public TileColumn[] Tiles
    {
        get { return m_Tiles;}
        set { m_Tiles = value; }
    }

    public int GetHeight()
    {
        if (m_Tiles == null || m_Tiles.Length == 0)
        {
            return 0;
        }

        return m_Tiles[0].Length;
    }

    public int GetWidth()
    {
        if (m_Tiles == null)
        {
            return 0;
        }

        return m_Tiles.Length;
    }

    public void Sincronise()
    {
        for (int i = 0; i < m_Tiles.Length; i++)
        {
            m_Tiles[i].Sincronise();
        }
    }

    public void BackUp()
    {
        Debug.Log("StartBackUp");
        for (int i = 0; i < m_Tiles.Length; i++)
        {
            m_Tiles[i].BackUp();
        }
    }
}

[System.Serializable]
public class TileColumn
{
    [SerializeField] private ETileType[] m_Tiles;
    private ETileType[] m_BackUp;

    public TileColumn(int aLength)
    {
        m_Tiles = new ETileType[aLength];
        m_BackUp = new ETileType[aLength];
    }

    public ETileType this[int aY]
    {
        get { return m_Tiles[aY]; }
        set { m_Tiles[aY] = value;
            
        }
    }

    public int Length { get { return m_Tiles == null ? 0 : m_Tiles.Length; } }

    public void BackUp()
    {
        for(int i = 0 ; i < m_Tiles.Length; i++)
        {
            m_Tiles[i] = m_BackUp[i];
        }
    }

    public void Sincronise()
    {
        for (int i = 0; i < m_Tiles.Length; i++)
        {
           m_BackUp[i] = m_Tiles[i];
        }
    }
}
