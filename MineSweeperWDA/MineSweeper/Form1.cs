using System;
using System.Collections;
using System.Drawing;
using System.Windows.Forms;

namespace MineSweeper
{
	public partial class Form1 : Form
	{
		private const int FieldWidth = 15;
		private const int FieldHeight = 8;
		private const string FlagText = "/~";
		private const string MineText = "●";
		private const string SecretText = "?";

		private static Color BeforeOpenColor = Color.Gray;
		private static Color AfterOpenColor = Color.Silver;
		private static Color MineColor = Color.Red;
		private static Color SecretColor = Color.LimeGreen;
		private static Color FontColor = Color.Black;
		private static Color FlagColor = Color.PeachPuff;
		private static Hashtable NumberColorTable;

		private Label[,] Labels;
		private MineSweeper MineSweeper;
		private bool IsGameOver;
		private Timer Timer;
		private int Time;
		private bool IsFirstClick;

		static Form1()
		{
			NumberColorTable = new Hashtable();
			NumberColorTable.Add(1, Color.Black);
			NumberColorTable.Add(2, Color.Green);
			NumberColorTable.Add(3, Color.Crimson);
			NumberColorTable.Add(4, Color.DarkBlue);
			NumberColorTable.Add(5, Color.DarkRed);
			NumberColorTable.Add(6, Color.LightSeaGreen);
			NumberColorTable.Add(7, Color.DarkViolet);
			NumberColorTable.Add(8, Color.PaleVioletRed);
		}

		public Form1()
		{
			InitializeComponent();
			SetLabels();

			Timer = new Timer();
			Timer.Interval = 100;

			Init();
		}

		public void Init()
		{
			MineSweeper = new MineSweeper(FieldHeight, FieldWidth);
			IsGameOver = false;
			IsFirstClick = true;

			Time = 0;
			counter.Text = CreateTimeFormat(Time);

			foreach (Label label in Labels)
			{
				label.Text = "";
				label.BackColor = BeforeOpenColor;
			}
		}

		private void StartTimer()
		{
			Timer.Tick += TimerTick;
			Timer.Start();
		}

		private void StopTimer()
		{
			Timer.Stop();
			Timer.Tick -= TimerTick;
		}

		private void TimerTick(object sender, EventArgs e)
		{
			counter.Text = CreateTimeFormat(++Time);
		}

		private void Open(int i, int j)
		{
			if (0 <= i && i < FieldHeight && 0 <= j && j < FieldWidth && Labels[i, j].BackColor == BeforeOpenColor)
			{
				Labels[i, j].ForeColor = FontColor;
				int status = MineSweeper.Open(i, j);
				if (status == -1)
				{
					// 地雷を踏んだ場合
					Explode(i, j);
					for (int k = 0; k < MineSweeper.FieldHeight; k++)
					{
						for (int l = 0; l < MineSweeper.FieldWidth; l++)
						{
							if (MineSweeper[k, l] == MineSweeper.Mine.Bomb)
							{
								Explode(k, l);
							}
						}
					}
					IsGameOver = true;
					StopTimer();
				}
				else if (status == -2)
				{
					// シークレットを踏んだ場合
					Labels[i, j].Text = SecretText;
					Labels[i, j].BackColor = SecretColor;
				}
				else if (status > 0)
				{
					// 周囲に地雷がある場合
					Labels[i, j].Text = "" + status;
					Labels[i, j].ForeColor = (Color)NumberColorTable[status];
					Labels[i, j].BackColor = AfterOpenColor;
				}
				else
				{
					// 周囲に地雷が無い場合
					Labels[i, j].Text = "";
					Labels[i, j].BackColor = AfterOpenColor;
					// 周囲を展開
					Open(i - 1, j - 1); // 左上
					Open(i - 1, j); // 上
					Open(i - 1, j + 1); // 右上
					Open(i, j - 1); // 左
					Open(i, j + 1); // 右
					Open(i + 1, j - 1); // 左下
					Open(i + 1, j); // 下
					Open(i + 1, j + 1); // 右下
				}
				if (MineSweeper.IsFinished())
				{
					StopTimer();
					IsGameOver = true;
					MessageBox.Show("Congratulations!");
				}
			}
		}

		private void Explode(int i, int j)
		{
			Labels[i, j].Text = MineText;
			Labels[i, j].ForeColor = FontColor;
			Labels[i, j].BackColor = MineColor;
		}

		private string CreateTimeFormat(int time)
		{
			string min;
			if (time / 600 < 10)
			{
				min = "0" + time / 600;
			}
			else
			{
				min = "" + time / 600;
			}
			string sec;
			if ((time % 600) / 10 < 10)
			{
				sec = "0" + (time % 600) / 10;
			}
			else
			{
				sec = "" + (time % 600) / 10;
			}
			string msec = "" + time % 10;
			return min + ":" + sec + "." + msec;
		}

		private void Reset(object sender, EventArgs e)
		{
			Reset();
		}

		private void Reset()
		{
			StopTimer();
			Init();
		}

		private void PanelClick(object sender, EventArgs e)
		{
			if (IsGameOver)
			{
				Reset();
			}
		}

		private void LabelClick(object sender, EventArgs e)
		{
			// 初回クリック時にタイマースタート
			if (IsFirstClick)
			{
				IsFirstClick = false;
				StartTimer();
			}
			MouseEventArgs me = (MouseEventArgs)e;
			Label label = (Label)sender;
			if (!IsGameOver && label.BackColor == BeforeOpenColor)
			{
				if (me.Button == MouseButtons.Left)
				{
					// 左クリック(Open)
					string tag = label.Name.Substring(5, label.Name.Length - 5);
					int i = int.Parse(tag.Split('_')[0]);
					int j = int.Parse(tag.Split('_')[1]);
					Open(i, j);
				}
				else if (label.Text.Equals(""))
				{
					// 右クリック(旗設置)
					label.ForeColor = FlagColor;
					label.Text = FlagText;
				}
				else if (label.Text.Equals(FlagText))
				{
					// 右クリック(旗撤去)
					label.ForeColor = FontColor;
					label.Text = "";
				}
			}
			else if (!IsGameOver && label.BackColor == AfterOpenColor && (me.Button == MouseButtons.Left || me.Button == MouseButtons.Right))
			{
				// フラグ以外周囲Open
				string tag = label.Name.Substring(5, label.Name.Length - 5);
				int i = int.Parse(tag.Split('_')[0]);
				int j = int.Parse(tag.Split('_')[1]);
				if (!Labels[i, j].Text.Equals(""))
				{
					int num = int.Parse(Labels[i, j].Text);
					int count = 0;
					bool upperLeft = !DetectFlag(i - 1, j - 1, ref count);
					bool top = !DetectFlag(i - 1, j, ref count);
					bool upperRight = !DetectFlag(i - 1, j + 1, ref count);
					bool left = !DetectFlag(i, j - 1, ref count);
					bool right = !DetectFlag(i, j + 1, ref count);
					bool lowerLeft = !DetectFlag(i + 1, j - 1, ref count);
					bool bottom = !DetectFlag(i + 1, j, ref count);
					bool lowerRight = !DetectFlag(i + 1, j + 1, ref count);
					if (count == num)
					{
						if (upperLeft)
						{
							Open(i - 1, j - 1);
						}
						if (top)
						{
							Open(i - 1, j);
						}
						if (upperRight)
						{
							Open(i - 1, j + 1);
						}
						if (left)
						{
							Open(i, j - 1);
						}
						if (right)
						{
							Open(i, j + 1);
						}
						if (lowerLeft)
						{
							Open(i + 1, j - 1);
						}
						if (bottom)
						{
							Open(i + 1, j);
						}
						if (lowerRight)
						{
							Open(i + 1, j + 1);
						}
					}
				}
			}
		}

		private bool DetectFlag(int i, int j, ref int count)
		{
			if (0 <= i && i < FieldHeight && 0 <= j && j < FieldWidth && Labels[i, j].Text == FlagText)
			{
				count++;
				return true;
			}
			return false;
		}

		private void SetLabels()
		{
			Labels = new Label[FieldHeight, FieldWidth];
			Labels[0, 0] = label0_0;
			Labels[0, 1] = label0_1;
			Labels[0, 2] = label0_2;
			Labels[0, 3] = label0_3;
			Labels[0, 4] = label0_4;
			Labels[0, 5] = label0_5;
			Labels[0, 6] = label0_6;
			Labels[0, 7] = label0_7;
			Labels[0, 8] = label0_8;
			Labels[0, 9] = label0_9;
			Labels[0, 10] = label0_10;
			Labels[0, 11] = label0_11;
			Labels[0, 12] = label0_12;
			Labels[0, 13] = label0_13;
			Labels[0, 14] = label0_14;
			Labels[1, 0] = label1_0;
			Labels[1, 1] = label1_1;
			Labels[1, 2] = label1_2;
			Labels[1, 3] = label1_3;
			Labels[1, 4] = label1_4;
			Labels[1, 5] = label1_5;
			Labels[1, 6] = label1_6;
			Labels[1, 7] = label1_7;
			Labels[1, 8] = label1_8;
			Labels[1, 9] = label1_9;
			Labels[1, 10] = label1_10;
			Labels[1, 11] = label1_11;
			Labels[1, 12] = label1_12;
			Labels[1, 13] = label1_13;
			Labels[1, 14] = label1_14;
			Labels[2, 0] = label2_0;
			Labels[2, 1] = label2_1;
			Labels[2, 2] = label2_2;
			Labels[2, 3] = label2_3;
			Labels[2, 4] = label2_4;
			Labels[2, 5] = label2_5;
			Labels[2, 6] = label2_6;
			Labels[2, 7] = label2_7;
			Labels[2, 8] = label2_8;
			Labels[2, 9] = label2_9;
			Labels[2, 10] = label2_10;
			Labels[2, 11] = label2_11;
			Labels[2, 12] = label2_12;
			Labels[2, 13] = label2_13;
			Labels[2, 14] = label2_14;
			Labels[3, 0] = label3_0;
			Labels[3, 1] = label3_1;
			Labels[3, 2] = label3_2;
			Labels[3, 3] = label3_3;
			Labels[3, 4] = label3_4;
			Labels[3, 5] = label3_5;
			Labels[3, 6] = label3_6;
			Labels[3, 7] = label3_7;
			Labels[3, 8] = label3_8;
			Labels[3, 9] = label3_9;
			Labels[3, 10] = label3_10;
			Labels[3, 11] = label3_11;
			Labels[3, 12] = label3_12;
			Labels[3, 13] = label3_13;
			Labels[3, 14] = label3_14;
			Labels[4, 0] = label4_0;
			Labels[4, 1] = label4_1;
			Labels[4, 2] = label4_2;
			Labels[4, 3] = label4_3;
			Labels[4, 4] = label4_4;
			Labels[4, 5] = label4_5;
			Labels[4, 6] = label4_6;
			Labels[4, 7] = label4_7;
			Labels[4, 8] = label4_8;
			Labels[4, 9] = label4_9;
			Labels[4, 10] = label4_10;
			Labels[4, 11] = label4_11;
			Labels[4, 12] = label4_12;
			Labels[4, 13] = label4_13;
			Labels[4, 14] = label4_14;
			Labels[5, 0] = label5_0;
			Labels[5, 1] = label5_1;
			Labels[5, 2] = label5_2;
			Labels[5, 3] = label5_3;
			Labels[5, 4] = label5_4;
			Labels[5, 5] = label5_5;
			Labels[5, 6] = label5_6;
			Labels[5, 7] = label5_7;
			Labels[5, 8] = label5_8;
			Labels[5, 9] = label5_9;
			Labels[5, 10] = label5_10;
			Labels[5, 11] = label5_11;
			Labels[5, 12] = label5_12;
			Labels[5, 13] = label5_13;
			Labels[5, 14] = label5_14;
			Labels[6, 0] = label6_0;
			Labels[6, 1] = label6_1;
			Labels[6, 2] = label6_2;
			Labels[6, 3] = label6_3;
			Labels[6, 4] = label6_4;
			Labels[6, 5] = label6_5;
			Labels[6, 6] = label6_6;
			Labels[6, 7] = label6_7;
			Labels[6, 8] = label6_8;
			Labels[6, 9] = label6_9;
			Labels[6, 10] = label6_10;
			Labels[6, 11] = label6_11;
			Labels[6, 12] = label6_12;
			Labels[6, 13] = label6_13;
			Labels[6, 14] = label6_14;
			Labels[7, 0] = label7_0;
			Labels[7, 1] = label7_1;
			Labels[7, 2] = label7_2;
			Labels[7, 3] = label7_3;
			Labels[7, 4] = label7_4;
			Labels[7, 5] = label7_5;
			Labels[7, 6] = label7_6;
			Labels[7, 7] = label7_7;
			Labels[7, 8] = label7_8;
			Labels[7, 9] = label7_9;
			Labels[7, 10] = label7_10;
			Labels[7, 11] = label7_11;
			Labels[7, 12] = label7_12;
			Labels[7, 13] = label7_13;
			Labels[7, 14] = label7_14;
		}
	}
}
