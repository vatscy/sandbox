using System;

namespace MineSweeper
{
	class MineSweeper
	{

		public const int DefaultHeight = 5;
		public const int DefaultWidth = 5;
		private const int MaxNumberOfMines = 30;
		private const int MaxNumberOfSecrets = 5;

		public enum Mine
		{
			Bomb,
			Secret,
			Nothing
		}

		public int FieldHeight
		{
			get;
			private set;
		}
		public int FieldWidth
		{
			get;
			private set;
		}
		private int OpenCount;
		private int MineCount;
		private bool IsSafe;
		private Mine[,] Mines;

		public MineSweeper()
		{
			FieldHeight = DefaultHeight;
			FieldWidth = DefaultWidth;
			Mines = new Mine[FieldHeight, FieldWidth];
			Init();
		}

		public MineSweeper(int size)
		{
			FieldHeight = size;
			FieldWidth = size;
			Mines = new Mine[FieldHeight, FieldWidth];
			Init();
		}

		public MineSweeper(int height, int width)
		{
			this.FieldHeight = height;
			this.FieldWidth = width;
			Mines = new Mine[height, width];
			Init();
		}

		public Mine this[int i, int j]
		{
			get
			{
				return Mines[i, j];
			}
		}

		public void Init()
		{
			IsSafe = true;
			OpenCount = 0;

			// Minesの初期化
			for (int i = 0; i < FieldHeight; i++)
				for (int j = 0; j < FieldWidth; j++)
					Mines[i, j] = Mine.Nothing;

			// 地雷を設置
			Random r = new Random();
			for (int i = 0; i < MaxNumberOfMines; i++)
				Mines[r.Next(0, FieldHeight), r.Next(0, FieldWidth)] = Mine.Bomb;

			// シークレットを設置
			for (int i = 0; i < MaxNumberOfSecrets; i++)
				Mines[r.Next(0, FieldHeight), r.Next(0, FieldWidth)] = Mine.Secret;

			// 爆弾の数を数える
			MineCount = 0;
			foreach (Mine mine in Mines)
				if (mine == Mine.Bomb)
					MineCount++;
		}

		public int Open(int i, int j)
		{
			OpenCount++;
			if (Mines[i, j] == Mine.Bomb)
			{
				IsSafe = false;
				return -1;
			}
			else if (Mines[i, j] == Mine.Secret)
			{
				return -2;
			}
			else
			{
				return CalcNumOfMines(i, j);
			}
		}

		private int CalcNumOfMines(int i, int j)
		{
			int count = 0;
			// 左上
			DetectMine(i - 1, j - 1, ref count);
			// 上
			DetectMine(i - 1, j, ref count);
			// 右上
			DetectMine(i - 1, j + 1, ref count);
			// 左
			DetectMine(i, j - 1, ref count);
			// 右
			DetectMine(i, j + 1, ref count);
			// 左下
			DetectMine(i + 1, j - 1, ref count);
			// 下
			DetectMine(i + 1, j, ref count);
			// 右下
			DetectMine(i + 1, j + 1, ref count);
			return count;
		}

		private void DetectMine(int i, int j, ref int count)
		{
			if (0 <= i && i < FieldHeight && 0 <= j && j < FieldWidth && Mines[i, j] == Mine.Bomb)
				count++;
		}

		public bool IsFinished()
		{
			return MineCount + OpenCount == FieldHeight * FieldWidth && IsSafe;
		}

		private class Settings
		{
			int MaxNumberOfMines
			{
				get;
				set;
			}
			int MaxNumberOfSecrets
			{
				get;
				set;
			}
			Settings(int mines, int secrets)
			{
				MaxNumberOfMines = mines;
				MaxNumberOfSecrets = secrets;
			}
		}

	}
}
