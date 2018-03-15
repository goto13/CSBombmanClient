using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace CSBombmanClient
{
	public class State
	{
		public int turn { get; set; }
		public List<int[]> walls { get; set; }
		public List<int[]> blocks { get; set; }
		public List<Player> players { get; set; }
		public List<Bomb> bombs { get; set; }
		public List<Item> items { get; set; }
		public List<int[]> fires { get; set; }

		public State(int turn,
			   List<int[]> walls,
			   List<int[]> blocks,
			   List<Player> players,
			   List<Bomb> bombs,
			   List<Item> items,
			   List<int[]> fires)
		{
			this.turn = turn;
			this.walls = walls;
			this.blocks = blocks;
			this.players = players;
			this.bombs = bombs;
			this.items = items;
			this.fires = fires;
		}
	}

	public class Player
	{
		public string name { get; set; }
		public Position pos;
		public int power;
		public int setBombLimit;
		public char ch;
		public bool isAlive;
		public int setBombCount;
		public int totalSetBombCount;
		public int id;

		public Player(string name,
					  Position pos,
					  int power,
					  int setBombLimit,
					  char ch,
					  bool isAlive,
					  int setBombCount,
					  int totalSetBombCount,
					  int id)
		{
			this.name = name;
			this.pos = pos;
			this.power = power;
			this.setBombLimit = setBombLimit;
			this.ch = ch;
			this.isAlive = isAlive;
			this.setBombCount = setBombCount;
			this.totalSetBombCount = totalSetBombCount;
			this.id = id;
		}
	}

	public class Position
	{
		public int x { get; set; }
		public int y { get; set; }

		public Position(int x, int y)
		{
			this.x = x;
			this.y = y;
		}

		public override bool Equals(System.Object obj)
		{
			// If parameter is null return false.
			if (obj == null)
			{
				return false;
			}

			// If parameter cannot be cast to Position return false.
			Position p = obj as Position;
			if ((System.Object)p == null)
			{
				return false;
			}

			// Return true if the fields match:
			return (x == p.x) && (y == p.y);
		}

		public bool Equals(Position p)
		{
			// If parameter is null return false:
			if ((object)p == null)
			{
				return false;
			}

			// Return true if the fields match:
			return (x == p.x) && (y == p.y);
		}

		public override int GetHashCode()
		{
			return x ^ y;
		}

		public override string ToString()
		{
			return $"[{x},{y}]";
		}

		public bool IsNext(Position pos)
		{
			return (pos.x == x && (pos.y + 1 == y || pos.y - 1 == y)) || (pos.y == y && (pos.x + 1 == x || pos.x - 1 == x));
		}
	}

	public class Block
	{
		public Position pos;

		public Block(Position pos)
		{
			this.pos = pos;
		}

		public override bool Equals(System.Object obj)
		{
			// If parameter is null return false.
			if (obj == null)
			{
				return false;
			}

			// If parameter cannot be cast to Block return false.
			Block b = obj as Block;
			if ((System.Object)b == null)
			{
				return false;
			}

			// Return true if the fields match:
			return pos.Equals(b.pos);
		}

		public bool Equals(Block b)
		{
			// If parameter is null return false:
			if ((object)b == null)
			{
				return false;
			}

			// Return true if the fields match:
			return pos.Equals(b.pos);
		}

		public override int GetHashCode()
		{
			return pos.GetHashCode();
		}
	}

	public class Bomb
	{
		public Position pos;
		public int timer;
		public int power;

		public Bomb(Position pos, int timer, int power)
		{
			this.pos = pos; // pos ha immutable
			this.power = power;
			this.timer = timer;
		}

		public override string ToString()
		{
			return "[" + pos.x + "," + pos.y + "]";
		}
	}

	public class Item
	{
		public Position pos;
		public char name;

		public Item(char name, Position pos)
		{
			this.pos = pos;
			this.name = name;
		}
	}
}
