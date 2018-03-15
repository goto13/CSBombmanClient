using System;
using System.Collections.Generic;
using System.Text;

namespace CSBombmanClient
{
	public class AI
	{
		private const string RIGHT = "RIGHT";
		private const string LEFT = "LEFT";
		private const string UP = "UP";
		private const string DOWN = "DOWN";

		private string previous = "";
		private List<int[]> posList = new List<int[]>();
		private bool reverse = false;
		private int reversedTurn = -1;
		private string vv = "";
		//private string bb = "";

		private int myId = -1;
		private int dummyId = 0;
		private Position myPos = null;
		private string message = "";

		private string ss = "{\"turn\":2,\"walls\":[[0,0],[0,1],[0,2],[0,3],[0,4],[0,5],[0,6],[0,7],[0,8],[0,9],[0,10],[0,11],[0,12],[0,13],[0,14],[1,0],[1,14],[2,0],[2,2],[2,4],[2,6],[2,8],[2,10],[2,12],[2,14],[3,0],[3,14],[4,0],[4,2],[4,4],[4,6],[4,8],[4,10],[4,12],[4,14],[5,0],[5,14],[6,0],[6,2],[6,4],[6,6],[6,8],[6,10],[6,12],[6,14],[7,0],[7,14],[8,0],[8,2],[8,4],[8,6],[8,8],[8,10],[8,12],[8,14],[9,0],[9,14],[10,0],[10,2],[10,4],[10,6],[10,8],[10,10],[10,12],[10,14],[11,0],[11,14],[12,0],[12,2],[12,4],[12,6],[12,8],[12,10],[12,12],[12,14],[13,0],[13,14],[14,0],[14,1],[14,2],[14,3],[14,4],[14,5],[14,6],[14,7],[14,8],[14,9],[14,10],[14,11],[14,12],[14,13],[14,14]],\"blocks\":[[3,2],[11,13],[2,9],[5,10],[13,7],[3,3],[1,9],[9,5],[12,7],[8,13],[5,4],[8,5],[4,11],[7,12],[3,11],[9,6],[2,3],[5,5],[2,11],[11,7],[1,3],[7,13],[7,6],[3,12],[6,13],[11,8],[7,7],[5,13],[1,4],[13,10],[4,5],[1,11],[9,8],[3,5],[11,2],[4,13],[10,9],[10,1],[6,7],[3,13],[9,9],[2,5],[5,7],[13,4],[1,5],[7,1],[4,7],[8,9],[11,11],[9,2],[5,8],[13,5],[10,11],[1,6],[9,3],[7,9],[5,1],[3,7],[8,3],[11,4],[7,10],[7,2],[3,8],[12,5],[9,11],[5,9],[11,5],[7,3],[3,1],[9,12],[6,3],[10,5],[8,11],[1,8],[5,3],[9,13],[3,10],[7,5],[3,4],[5,12],[13,9],[12,9],[11,1],[11,10],[13,3],[6,1],[6,9],[4,1],[2,7],[13,6]],\"players\":[{\"name\":\"敵\",\"pos\":{\"x\":1,\"y\":2},\"power\":2,\"setBombLimit\":2,\"ch\":\"敵\",\"isAlive\":true,\"setBombCount\":0,\"totalSetBombCount\":0,\"id\":0},{\"name\":\"何か\",\"pos\":{\"x\":1,\"y\":12},\"power\":2,\"setBombLimit\":2,\"ch\":\"何\",\"isAlive\":true,\"setBombCount\":2,\"totalSetBombCount\":2,\"id\":1},{\"name\":\"あなた\",\"pos\":{\"x\":13,\"y\":1},\"power\":2,\"setBombLimit\":2,\"ch\":\"あ\",\"isAlive\":true,\"setBombCount\":0,\"totalSetBombCount\":0,\"id\":2},{\"name\":\"何か\",\"pos\":{\"x\":13,\"y\":11},\"power\":2,\"setBombLimit\":2,\"ch\":\"何\",\"isAlive\":true,\"setBombCount\":2,\"totalSetBombCount\":2,\"id\":3}],\"bombs\":[{\"pos\":{\"x\":1,\"y\":13},\"timer\":8,\"power\":2},{\"pos\":{\"x\":13,\"y\":13},\"timer\":8,\"power\":2},{\"pos\":{\"x\":1,\"y\":12},\"timer\":9,\"power\":2},{\"pos\":{\"x\":13,\"y\":12},\"timer\":9,\"power\":2}],\"items\":[],\"fires\":[]}";

		public AI()
		{
		}

		//public void Run()
		//{
		//	myId = 3;
		//	State state = null;
		//	try
		//	{
		//		state = Newtonsoft.Json.JsonConvert.DeserializeObject<State>(ss);
		//	}
		//	catch (Exception e)
		//	{
		//		state = null;
		//		//s = e.StackTrace;
		//	}

		//	//Console.WriteLine(moves[rand.Next(0, 3)] + ",false");
		//	if (state != null)
		//	{
		//		var res = DoSome(state);
		//		Console.WriteLine("UP,true," + ss);
		//	}
		//	else
		//	{
		//		Console.WriteLine("UP,true," + ss);
		//	}
		//	Console.ReadLine();
		//}

		public void Run2()
		{
			Console.InputEncoding = Encoding.UTF8;
			Console.OutputEncoding = Encoding.UTF8;
			Console.WriteLine("何か", Console.OutputEncoding.CodePage);

			vv = Console.ReadLine();
			//byte[] data = Encoding.UTF8.GetBytes(vv);
			//for (int i = 0; i < data.Length; i++)
			//{
			//	bb += Convert.ToString(data[i], 16) + " - ";
			//}

			//string z = vv;
			//// id
			//try
			//{

			//dummyId = int.Parse(z);
			//}
			//catch
			//{
			//	;
			//}
			//int.TryParse(vv, out dummyId);

			// 標準入力
			string[] moves = { "UP", "DOWN", "LEFT", "RIGHT" };
			//var rand = new Random();
			//var ai = new AI();
			State state = null;

			var s = Console.ReadLine();
			try
			{
				state = Newtonsoft.Json.JsonConvert.DeserializeObject<State>(s);
				SetId(state);
			}
			catch (Exception e)
			{
				state = null;
				//s = e.StackTrace;
			}

			// Console.WriteLine(moves[rand.Next(0, 3)] + ",false");
			if (state != null)
			{
				var res = DoSome(state);
				previous = res;
				Console.WriteLine(res);
			}
			else
			{
				Console.WriteLine("UP,false,hello");
			}


			while (true)
			{
				var ss = Console.ReadLine();
				try
				{
					state = Newtonsoft.Json.JsonConvert.DeserializeObject<State>(ss);
				}
				catch (Exception e)
				{
					state = null;
					//s = e.StackTrace;
				}

				// Console.WriteLine(moves[rand.Next(0, 3)] + ",false");
				if (state != null)
				{
					var res = DoSome(state);
					previous = res;
					Console.WriteLine(res);
				}
				else
				{
					Console.WriteLine("UP,false,hello");
				}
			}
		}

		private void SetId(State state)
		{
			foreach (var p in state.players)
			{
				if (p.name != "何か")
					continue;
				myId = p.id;
			}
		}

		private string DoSome(State state)
		{
			message = "";

			if (state.turn < 5)
			{
				try
				{
					int.TryParse(vv, out int x);
					var l = vv.Length;
					message = "/vv/" + vv + "/dummy/" + x + "/real/" + myId;
				}
				catch (Exception e)
				{
					//vv = e.Message;
				}

			}

			//if (state.turn == 1)
			//{
			//	var z = MyPos(state);
			//	var zz = GetMy(state);
			//	return "STAY,false,id:" + myId + "/px:" + z.x + "/py:" + z.y + "//" + zz.id + "/" + zz.pos.x + "/" + zz.pos.y;
			//}

			myPos = MyPos(state);
			posList.Add(new int[] { myPos.x, myPos.y });

			// 右下左上の順で移動していく
			var array = GetDirectionOrder(myPos, state);
			return DoMove(array, myPos, state);
			//{
			//	return array[0] + ",false";
			//}
			//else if (CanMove(array[1], myPos, state))
			//{
			//	return array[1] + ",false";
			//}
			//// 2回壁にぶつかったら、戻ることになるので爆弾置く
			//else if (CanMove(array[2], myPos, state))
			//{
			//	return array[2] + ",true";
			//}

			//return "UP, false, hello";
		}

		private string[] GetDirectionOrder(Position myPos, State state)
		{
			if (string.IsNullOrEmpty(previous))
			{
				if (myPos.x == 1 && myPos.y == 1)
				{
					return new string[] { RIGHT, DOWN, LEFT, UP };
				}
				else if (myPos.x == 1 && myPos.y == 13)
				{
					return new string[] { UP, RIGHT, DOWN, LEFT };
				}
				else if (myPos.x == 13 && myPos.y == 1)
				{
					return new string[] { DOWN, LEFT, UP, RIGHT };
				}
				else
				{
					return new string[] { LEFT, UP, RIGHT, DOWN };
				}
			}

			if (10 < posList.Count && reversedTurn + 8 < state.turn)
			{
				var size = posList.Count;
				var now = posList[size - 1];
				var prev = posList[size - 2];

				var sameList = new List<int>();
				for (var i = size - 3; 0 < i; i--)
				{
					if (posList[i][0] == now[0] && posList[i][1] == now[1])
					{
						if (posList[i - 1][0] == prev[0] && posList[i - 1][1] == prev[1])
						{
							sameList.Add(size - 1 - i);
							if (sameList.Count == 2)
								break;
						}
					}
				}
				if (sameList.Count == 2)
				{
					if (sameList[0] * 2 == sameList[1])
					{
						reversedTurn = state.turn;
						message = "reversed!";
						reverse = !reverse;
					}
					else
					{
						message = "samed:" + sameList[0] + "/" + sameList[1];
					}
				}
			}

			if (!reverse)
			{
				// changed turn
				if (reversedTurn == state.turn)
				{
					if (previous.StartsWith("R"))
					{
						return new string[] { LEFT, UP, RIGHT, DOWN, };
					}
					else if (previous.StartsWith("D"))
					{
						return new string[] { UP, RIGHT, DOWN, LEFT, };
					}
					else if (previous.StartsWith("L"))
					{
						return new string[] { RIGHT, DOWN, LEFT, UP, };
					}
					else
					{
						return new string[] { DOWN, LEFT, UP, RIGHT, };
					}
				}
				else
				{
					if (previous.StartsWith("R"))
					{
						return new string[] { RIGHT, DOWN, LEFT, UP };
					}
					else if (previous.StartsWith("D"))
					{
						return new string[] { DOWN, LEFT, UP, RIGHT };
					}
					else if (previous.StartsWith("L"))
					{
						return new string[] { LEFT, UP, RIGHT, DOWN };
					}
					else
					{
						return new string[] { UP, RIGHT, DOWN, LEFT };
					}
				}
			}
			else
			{
				// changed turn
				if (reversedTurn == state.turn)
				{
					if (previous.StartsWith("R"))
					{
						return new string[] { LEFT, DOWN, RIGHT, UP };
					}
					else if (previous.StartsWith("D"))
					{
						return new string[] { UP, LEFT, DOWN, RIGHT };
					}
					else if (previous.StartsWith("L"))
					{
						return new string[] { RIGHT, UP, LEFT, DOWN };
					}
					else
					{
						return new string[] { DOWN, RIGHT, UP, LEFT };
					}
				}
				else
				{
					if (previous.StartsWith("R"))
					{
						return new string[] { RIGHT, UP, LEFT, DOWN };
					}
					else if (previous.StartsWith("D"))
					{
						return new string[] { DOWN, RIGHT, UP, LEFT };
					}
					else if (previous.StartsWith("L"))
					{
						return new string[] { LEFT, DOWN, RIGHT, UP };
					}
					else
					{
						return new string[] { UP, LEFT, DOWN, RIGHT };
					}
				}
			}
		}

		private string DoMove(string[] directions, Position myPos, State state)
		{
			var nextPos = GetNextPositions(myPos, directions[0]);
			//return $"STAY,false, {nextPos[0][0]}_{nextPos[0][1]} {nextPos[1][0]}_{nextPos[1][1]} {nextPos[2][0]}_{nextPos[2][1]} {nextPos[3][0]}_{nextPos[3][1]}";
			var canMove = new bool[] { true, true, true, true };

			foreach (var wall in state.walls)
			{
				if (canMove[0] && wall[0] == nextPos[0][0] && wall[1] == nextPos[0][1])
					canMove[0] = false;
				if (canMove[1] && wall[0] == nextPos[1][0] && wall[1] == nextPos[1][1])
					canMove[1] = false;
				if (canMove[2] && wall[0] == nextPos[2][0] && wall[1] == nextPos[2][1])
					canMove[2] = false;
				if (canMove[3] && wall[0] == nextPos[3][0] && wall[1] == nextPos[3][1])
					canMove[3] = false;
			}

			foreach (var wall in state.blocks)
			{
				if (canMove[0] && wall[0] == nextPos[0][0] && wall[1] == nextPos[0][1])
					canMove[0] = false;
				if (canMove[1] && wall[0] == nextPos[1][0] && wall[1] == nextPos[1][1])
					canMove[1] = false;
				if (canMove[2] && wall[0] == nextPos[2][0] && wall[1] == nextPos[2][1])
					canMove[2] = false;
				if (canMove[3] && wall[0] == nextPos[3][0] && wall[1] == nextPos[3][1])
					canMove[3] = false;
			}

			foreach (var bomb in state.bombs)
			{
				var bPos = bomb.pos;
				if (canMove[0] && bPos.x == nextPos[0][0] && bPos.y == nextPos[0][1])
					canMove[0] = false;
				if (canMove[1] && bPos.x == nextPos[1][0] && bPos.y == nextPos[1][1])
					canMove[1] = false;
				if (canMove[2] && bPos.x == nextPos[2][0] && bPos.y == nextPos[2][1])
					canMove[2] = false;
				if (canMove[3] && bPos.x == nextPos[3][0] && bPos.y == nextPos[3][1])
					canMove[3] = false;
			}



			var dangerousDirs = GetDangerousDirection(myPos, state);
			var d = string.Join("_", dangerousDirs);
			if (state.turn < 3)
			{
				if (canMove[0] && !dangerousDirs.Contains(directions[0]))
					return directions[0] + ",false," + state.turn + message;
				else if (canMove[1] && !dangerousDirs.Contains(directions[1]))
					return directions[1] + ",false," + state.turn + message;
				else if (canMove[2] && !dangerousDirs.Contains(directions[2]))
					return directions[2] + ",false," + state.turn + message;
				else if (canMove[3] && !dangerousDirs.Contains(directions[3]))
					return directions[3] + ",false," + state.turn + message;
				else
				{
					var pDir = previous == null ? "" : previous.Substring(0, 1);
					//var d = string.Join(",", dangerousDirs);
					return $"STAY,false,p:{pDir}/d:{d}";
				}
			}
			else
			{
				if (canMove[0] && !dangerousDirs.Contains(directions[0]))
					return directions[0] + ",false," + state.turn + message;
				else if (canMove[1] && !dangerousDirs.Contains(directions[1]))
					return directions[1] + ",false," + state.turn + message;
				else if (canMove[2] && !dangerousDirs.Contains(directions[2]))
					return directions[2] + (IsOk(directions[2], myPos, canMove[3], state) ? ",true," : ",false,") + state.turn + message;
				else if (canMove[3] && !dangerousDirs.Contains(directions[3]))
					return directions[3] + ",false," + state.turn + message;
				else
				{
					var pDir = previous == null ? "" : previous.Substring(0, 1);
					//var d = string.Join(",", dangerousDirs);
					return $"STAY,false,p:{pDir}/d:{d}";
				}
			}
		}

		private bool IsOk(string dir, Position myPos, bool canMoveToNotBack, State state)
		{
			if (canMoveToNotBack)
				return false;

			foreach (var bomb in state.bombs)
			{
				if (bomb.pos.x == myPos.x || bomb.pos.y == myPos.y)
					return false;
			}
			// not 角
			if ((myPos.x == 1 && (myPos.y == 1 || myPos.y == 13))
				|| (myPos.x == 13 && (myPos.y == 1 || myPos.y == 13)))
				return false;

			foreach (var p in state.players)
			{
				if (p.name != "何か")
					continue;
				if (0 < p.setBombCount)
					return false;
				break;
			}

			if (1 < state.fires.Count)
				return false;

			return true;
		}

		private List<string> GetDangerousDirection(Position myPos, State state)
		{
			var dirs = new List<string>();
			foreach (var bomb in state.bombs)
			{
				// 5手づめするやつまだいないだろう・・・
				if (5 < bomb.timer)
					continue;
				var bPos = bomb.pos;
				if (bPos.x == myPos.x)
				{
					if (bPos.y - bomb.power - 2 < myPos.y && myPos.y < bPos.y + bomb.power + 2)
					{
						// 自機が爆風直線の下側にいる
						if (bPos.y < myPos.y)
							dirs.Add(UP);
						else
							dirs.Add(DOWN);
					}
				}
				else if (bPos.y == myPos.y)
				{
					if (bPos.x - bomb.power - 2 < myPos.x && myPos.x < bPos.x + bomb.power + 2)
					{
						// 自機が爆風直線の右側にいる
						if (bPos.x < myPos.x)
							dirs.Add(LEFT);
						else
							dirs.Add(RIGHT);
					}
				}
				// move to on the bomb line
				else if (bPos.y + 1 == myPos.y || bPos.y - 1 == myPos.y)
				{
					if (bPos.x - bomb.power <= myPos.x && myPos.x <= bPos.x + bomb.power)
					{
						// 自機が爆風直線の下側にいる
						if (bPos.y < myPos.y)
							dirs.Add(UP);
						else
							dirs.Add(DOWN);
					}
				}
				else if (bPos.x + 1 == myPos.x || bPos.x - 1 == myPos.x)
				{
					if (bPos.y - bomb.power <= myPos.y && myPos.y <= bPos.y + bomb.power)
					{
						// 自機が爆風直線の右側にいる
						if (bPos.x < myPos.x)
							dirs.Add(LEFT);
						else
							dirs.Add(RIGHT);
					}
				}
			}
			foreach (var fire in state.fires)
			{
				// right is fire
				if (myPos.x + 1 == fire[0] && myPos.y == fire[1])
				{
					dirs.Add(RIGHT);
				}
				// left is fire
				else if (myPos.x - 1 == fire[0] && myPos.y == fire[1])
				{
					dirs.Add(LEFT);
				}
				// up is fire
				else if (myPos.x == fire[0] && myPos.y - 1 == fire[1])
				{
					dirs.Add(UP);
				}
				else if (myPos.x == fire[0] && myPos.y + 1 == fire[1])
				{
					dirs.Add(DOWN);
				}
			}
			return dirs;
		}

		private int[][] GetNextPositions(Position myPos, string direction)
		{
			// R
			var r = new int[] { myPos.x + 1, myPos.y };
			// D
			var d = new int[] { myPos.x, myPos.y + 1 };
			// L
			var l = new int[] { myPos.x - 1, myPos.y };
			// U
			var u = new int[] { myPos.x, myPos.y - 1 };

			if (!reverse)
			{
				switch (direction)
				{
					case RIGHT:
						return new int[][] { r, d, l, u };
					case LEFT:
						return new int[][] { l, u, r, d };
					case UP:
						return new int[][] { u, r, d, l };
					case DOWN:
						return new int[][] { d, l, u, r };
					default:
						return null;
				}
			}
			else
			{
				switch (direction)
				{
					case RIGHT:
						return new int[][] { r, u, l, d };
					case LEFT:
						return new int[][] { l, d, r, u };
					case UP:
						return new int[][] { u, l, d, r };
					case DOWN:
						return new int[][] { d, r, u, l };
					default:
						return null;
				}
			}

		}

		//private int[][] DangerousArea(State state)
		//{
		//	foreach (var bomb in state.bombs)
		//	{
		//		if (bomb.timer < 5)
		//		{
		//			// up, down, left, right
		//			var area = SimpleBombArea(bomb);
		//			//Console.WriteLine("DOWN,true," + powers);
		//			if (InBombFire(powers, bomb.pos, state.players[myId].pos))
		//			{

		//			}
		//		}
		//	}
		//	return new int[][] { };
		//}

		//private int[] SimpleBombArea(Bomb bomb)
		//{
		//	return 
		//}


		private bool CanMove(string direction, Position myPos, State state)
		{
			var nextPos = GetNextPos(myPos, direction);
			if (WallContain(state, nextPos[0], nextPos[1]))
			{
				return false;
			}
			else if (BlockContain(state, nextPos[0], nextPos[1]))
			{
				return false;
			}

			return true;
		}


		private int[] GetNextPos(Position myPos, string direction)
		{
			switch (direction)
			{
				case RIGHT:
					return new int[] { myPos.x + 1, myPos.y };
				case LEFT:
					return new int[] { myPos.x - 1, myPos.y };
				case UP:
					return new int[] { myPos.x, myPos.y - 1 };
				case DOWN:
					return new int[] { myPos.x, myPos.y + 1 };
				default:
					return null;
			}
		}

		private Position MyPos(State state)
		{
			return state.players[myId].pos;
		}

		private string Escape(State state)
		{
			// TODO
			// 爆弾の死角に行く
			// 5歩先の安全地帯
			var safePositions = new List<Position>();
			for (var dx = -5; dx < 6; dx++)
			{
				for (var dy = -5; dy < 6; dy++)
				{
					if (WallOrBlockContain(state, myPos.x + dx, myPos.y + dy))
						continue;

					safePositions.Add(new Position(myPos.x + dx, myPos.y + dy));
				}
			}

			// 安全地帯への経路探索
			// 遠い順
			//// myPosからつながってるもの

			var dic = ByDistance(safePositions, myPos);
			//safePositions = CanReachables(dic, myPos);
			var path = GetPath(myPos, safePositions[0], state);
			if (myPos.x - path[0].x == 1)
				return "LEFT, false, dangerous";
			if (myPos.x - path[0].x == -1)
				return "RIGHT, false, dangerous";
			if (myPos.y - path[0].y == 1)
				return "UP, false, dangerous";
			if (myPos.y - path[0].y == -1)
				return "DOWN, false, dangerous";

			GetWays(state, safePositions);

			foreach (var bomb in state.bombs)
			{
				if (bomb.timer < 10)
				{
					// up, down, left, right
					var powers = BombPowers(state, bomb);
					//Console.WriteLine("DOWN,true," + powers);
					if (InBombFire(powers, bomb.pos, state.players[myId].pos))
					{
						return "STAY, false, nooo";
					}
				}
			}
			return "DOWN, false";
		}

		//private List<Position> CanReachables(Dictionary<int, List<Position>> dic, Position myPos)
		//{
		//	var distanceList = new List<int>();
		//	for (int i = 5; 0 < i; i--)
		//	{
		//		if (dic.ContainsKey(i))
		//			distanceList.Add(i);
		//		else
		//			distanceList.Clear();
		//	}
		//	var trees = new Dictionary<int, List<PathTree>>();
		//	foreach (var distance in distanceList)
		//	{
		//		if (distance == 1)
		//			continue;

		//		var list = dic[distance];
		//		var nearList = dic[distance - 1];
		//		foreach (var p in list)
		//		{
		//			var tree = new PathTree(p);
		//			foreach (var nearP in nearList)
		//			{
		//				if (p.IsNext(nearP))
		//					tree.nearPosList.Add(nearP);
		//			}
		//			if (tree.nearPosList.Count == 0)
		//				continue;

		//			if (!trees.ContainsKey(distance))
		//			{
		//				trees.Add(distance, new List<PathTree>());
		//			}
		//			trees[distance].Add(tree);
		//		}
		//	}

		//	var pathList = new List<int>();
		//	for (int i = 5; 0 < i; i--)
		//	{
		//		if (trees.ContainsKey(i))
		//			pathList.Add(i);
		//		else
		//			pathList.Clear();
		//	}
		//	int farDis = pathList[0];
		//	//foreach (var tree in trees[farDis])
		//	//{
		//	//	do
		//	//	{


		//	//	} while (farDis == 1)
		//	//}
		//}

		//private bool CanReach(Dictionary<int, List<PathTree>> trees, int distance)
		//{
		//	foreach (var tree in trees[distance])
		//	{
		//		var list = trees[distance - 1];
		//		foreach(var tree in list)
		//		{
		//			tree.pos.Equals()
		//		}
		//	}
		//}

		private class PathTree
		{
			public Position pos;
			public List<Position> nearPosList;

			public PathTree(Position pos, List<Position> nearPosList)
			{
				this.pos = pos;
				this.nearPosList = nearPosList;
			}

			public PathTree(Position pos)
			{
				this.pos = pos;
				this.nearPosList = new List<Position>();
			}
		}


		public List<Position> GetPath(Position start, Position goal, State state)
		{
			//bool[,] path = new bool[13, 13];
			List<Position> path = new List<Position>();
			List<int[]> wall2 = new List<int[]>(state.walls);
			wall2.AddRange(state.blocks);
			//bool[,] wall2 = (bool[,])wall.Clone();
			Position[] move = { new Position(1, 0), new Position(0, 1), new Position(-1, 0), new Position(0, -1) };

			Queue<Location> queue = new Queue<Location>();
			queue.Enqueue(new Location(start, 0, null));
			wall2.Add(new int[] { start.x, start.y });

			while (0 < queue.Count)
			{
				Location location = queue.Dequeue();
				if (location.here == goal)
				{
					while (location != null)
					{
						//path[location.here.x, location.here.y] = true;
						location = location.before;
					}
					break;
				}
				else
				{
					for (int i = 0; i < move.Length; i++)
					{
						Position next = location.here;
						next = new Position(next.x + move[i].x, next.y + move[i].y);
						//next.Offset(move[i]);
						if (!wall2.Contains(new int[] { next.x, next.y }))
						{
							queue.Enqueue(new Location(next, location.distance + 1, location));
							wall2.Add(new int[] { next.x, next.y });
							path.Add(new Position(next.x, next.y));
						}
					}
				}
			}
			return path;
		}

		private class Location
		{
			public Position here;
			public int distance;
			public Location before;

			public Location(Position here, int distance, Location before)
			{
				this.here = here;
				this.distance = distance;
				this.before = before;
			}
		}

		private void GetWays(State state, List<Position> safePositions)
		{


			// まず、4隅にいる場合
			// 左上
			if (myPos.Equals(new Position(1, 1)))
			{
				foreach (Position pos in safePositions)
				{
					// 1,3,5,7,9,11,13の箇所にまず移動
					var x = (pos.x % 2 == 0) ? pos.x - 1 : pos.x;
					var y = (pos.y % 2 == 0) ? pos.y - 1 : pos.y;
					//(x - myPos.x) / 2 
				}
			}
			else if (myPos.Equals(new Position(1, 2)))
			{

			}
			else if (myPos.Equals(new Position(1, 3)))
			{

			}
			else if (myPos.Equals(new Position(2, 1)))
			{

			}
			else if (myPos.Equals(new Position(3, 1)))
			{

			}
		}

		private bool IsDangerous(State state)
		{
			// TODO
			if (state.bombs.Count == 0)
				return false;
			// 火の通り道と自分の位置を確認
			foreach (var bomb in state.bombs)
			{
				if (bomb.timer < 10)
				{
					// up, down, left, right
					var powers = BombPowers(state, bomb);
					//Console.WriteLine("DOWN,true," + powers);
					if (InBombFire(powers, bomb.pos, state.players[myId].pos))
					{
						return true;
					}
				}
			}



			return false;
		}

		private bool InBombFire(Tuple<int, int, int, int> powers, Position bombPos, Position playerPos)
		{
			return (bombPos.y - powers.Item1 <= playerPos.y && playerPos.y <= bombPos.y + powers.Item2)
				&& (bombPos.x - powers.Item3 <= playerPos.x && playerPos.x <= bombPos.x + powers.Item4);
		}

		/// <summary>
		/// up, down, left, right
		/// </summary>
		/// <param name="state"></param>
		/// <param name="bomb"></param>
		/// <returns></returns>
		private Tuple<int, int, int, int> BombPowers(State state, Bomb bomb)
		{
			var up = bomb.power;
			for (int i = 1; i <= bomb.power; i++)
			{
				if (WallOrBlockContain(state, bomb.pos.x, bomb.pos.y - i))
				{
					up = i - 1;
					break;
				}
			}

			var down = bomb.power;
			for (int i = 1; i <= bomb.power; i++)
			{
				if (WallOrBlockContain(state, bomb.pos.x, bomb.pos.y + i))
				{
					down = i - 1;
					break;
				}
			}
			var left = bomb.power;
			for (int i = 1; i < bomb.power; i++)
			{
				if (WallOrBlockContain(state, bomb.pos.x - i, bomb.pos.y))
				{
					left = i - 1;
					break;
				}
			}
			var right = bomb.power;
			for (int i = 1; i < bomb.power; i++)
			{
				if (WallOrBlockContain(state, bomb.pos.x + i, bomb.pos.y))
				{
					right = i - 1;
					break;
				}
			}

			return Tuple.Create(up, down, left, right);
		}

		private bool WallContain(State state, int x, int y)
		{
			foreach (var wall in state.walls)
			{
				if (wall[0] == x && wall[1] == y)
					return true;
			}
			return false;
		}

		private bool BlockContain(State state, int x, int y)
		{
			foreach (var block in state.blocks)
			{
				if (block[0] == x && block[1] == y)
					return true;
			}
			return false;
		}


		private bool WallOrBlockContain(State state, int x, int y)
		{
			return WallContain(state, x, y) || BlockContain(state, x, y);
		}

		private Dictionary<int, List<Position>> ByDistance(List<Position> list, Position pos)
		{
			var result = new Dictionary<int, List<Position>>();
			foreach (var p in list)
			{
				var distance = Math.Abs(p.x - pos.x) + Math.Abs(p.y - pos.y);
				if (result.ContainsKey(distance))
				{
					var l = new List<Position>();
					l.Add(p);
					result.Add(distance, l);
				}
				else
				{
					var l = result[distance];
					l.Add(p);
					//result.Add(distance, l);
				}
			}
			return result;
		}
	}
}
