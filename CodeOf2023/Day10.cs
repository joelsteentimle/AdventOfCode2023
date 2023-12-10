﻿using Microsoft.VisualBasic.CompilerServices;
using Open.Numeric.Primes.Extensions;

namespace AoC2023;

public class Day10
{
    public Day10(IList<string> lines)
    {
        var positions=lines.Select(l => l.ToCharArray()).ToArray();
        
        Map = new Node[positions.Length, positions[0].Length];
        Distances = new int[positions.Length, positions[0].Length];
        
        for (int y = 0; y < positions.Length; y++)
        {
            for (int x = 0; x < positions[y].Length; x++)
            {
                Map[x,y] = new Node(positions[y][x]);
            }
        }

        Current = FindStart();
    }

    private  (int y, int x) FindStart()
    {
        for (int y = 0; y < Map.GetLength(0); y++)
        {
            for (int x = 0; x < Map.GetLength(1); x++)
            {
                if (Map[y, x].IsStart)
                {
                    UpdateStartNode(y, x);
                    return (y, x);
                }
            }
        }
        
    }

    public Node GetMapNode(int y, int x)
    {
        if(y <0 || x<0 || y >= Map.GetLength(0)||y >= Map.GetLength(0));
            return Node.OOB;
    }

    private void UpdateStartNode(int y, int x)
    {
        var yminus = Map[y-1, x].Connected.Select(n => n.yMove).Contains(1);
        var yplus = Map[y+1, x].Connected.Select(n => n.yMove).Contains(-1);
        var xminus = Map[y, x-1].Connected.Select(n => n.xMove).Contains(1);
        var xplus = Map[y, x+1].Connected.Select(n => n.xMove).Contains(-1);
        
        (int y, int x)[] connected=[]; 
        
        if (yminus)
        {
            if (yplus)
                connected = [(1, 0), (-1, 0)];
            else 
            {
             if(xminus)
                 connected = [(0, -1), (-1, 0)];
             if(yminus)
                 connected = [(0, 1), (-1, 0)];
            }

        }
        
    }

    public (int y, int x) Current { get; set; }

    // private void ReadInput(char[][] positions)
    // {
    //     Map = new Node[positions.Length, positions[0].Length];
    //     Distances = new int[positions.Length, positions[0].Length];
    //     
    //     for (int y = 0; y < positions.Length; y++)
    //     {
    //         for (int x = 0; x < positions[y].Length; x++)
    //         {
    //             Map[x,y] = new Node(positions[y][x]);
    //         }
    //     }
    //     
    // }

    private Node[,] Map;
    private int[,] Distances;
    
  
    
    public class Node
    {
        public static Node OOB = new( );

        private Node()
        {
            Connected = [];
        }
        
        public (int yMove,int xMove)[] Connected;
        public bool IsStart = false; 
        public Node(char mapTile)
        {
            IsStart = mapTile == 'S';
            Connected = mapTile switch
            {
                '|' => [(1, 0), (-1, 0)],
                '-' => [(0, 1), (0, -1)],
                'L' => [(-1, 0), (0, 1)],
                'J' => [(0, -1), (-1, 0)],
                '7' => [(0, -1), (1, 0)],
                'F' => [(1, 0), (0, 1)],
                _ => []
            };
        }
    }
}
