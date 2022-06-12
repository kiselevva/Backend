using Xunit;
using ScrumBoard;
using System;

namespace ScrumBoardTests
{
    public class UnitTest1
    {
        [Fact]
        public void CreateBoard()
        {
            ScrumBoard.ScrumBoard board = new ScrumBoard.ScrumBoard("Test Name");
            Assert.Equal("Test Name", board.GetName());
        }

        [Fact]
        public void AddColumn_to_new_board()
        {
            ScrumBoard.ScrumBoard board = new ScrumBoard.ScrumBoard("Test Name");
            board.AddColumn("name");

            Assert.Equal("name", board.GetColumn(0).GetName());
        }
        [Fact]
        public void AddColumn_to_fulled_board()
        {
            ScrumBoard.ScrumBoard board = new ScrumBoard.ScrumBoard("Test Name");
            for (int i = 0; i < 9; i++)
            {
                board.AddColumn("test");
            }

            Assert.Throws<Exception>(() => board.AddColumn("test"));
        }

        [Fact]
        public void DeleteColumn_which_exists()
        {
            ScrumBoard.ScrumBoard board = new ScrumBoard.ScrumBoard("Test Name");
            board.AddColumn("name");

            board.DeleteColumn("name");

            Assert.Empty(board.GetColumns());
        }
        [Fact]
        public void DeleteColumn_which_not_exist()
        {
            ScrumBoard.ScrumBoard board = new ScrumBoard.ScrumBoard("Test Name");

            Assert.Throws<Exception>(() => board.DeleteColumn("name"));
        }
        [Fact]
        public void AddTask_when_column_exist()
        {
            ScrumBoard.ScrumBoard board = new ScrumBoard.ScrumBoard("Test Name");
            board.AddColumn("name");

            board.AddTask("name", "descr", Priority.low);

            Assert.Equal("name", board.GetColumn(0).GetTasks()[0].Title);
            Assert.Equal("descr", board.GetColumn(0).GetTasks()[0].Description);
            Assert.Equal(Priority.low, board.GetColumn(0).GetTasks()[0].Priority);
        }
        [Fact]
        public void AddTask_when_columns_not_exist()
        {
            ScrumBoard.ScrumBoard board = new ScrumBoard.ScrumBoard("Test Name");

            Assert.Throws<Exception>(() => board.AddTask("name", "descr", Priority.low));
        }

        [Fact]
        public void MoveTask_to_exist_columns()
        {
            ScrumBoard.ScrumBoard board = new ScrumBoard.ScrumBoard("Test Name");
            board.AddColumn("1");
            board.AddColumn("2");
            board.AddTask("name", "descr", Priority.low);

            board.MoveTask("name", "2");

            Assert.Equal("name", board.GetColumn(1).GetTasks()[0].Title);
        }
        [Fact]
        public void MoveTask_to_not_exist_columns()
        {
            ScrumBoard.ScrumBoard board = new ScrumBoard.ScrumBoard("Test Name");
            board.AddColumn("1");

            board.AddTask("name", "descr", Priority.low);

            Assert.Throws<Exception>(() => board.MoveTask("name", "3"));
        }
        [Fact]
        public void MoveTask_when_task_not_exist()
        {
            ScrumBoard.ScrumBoard board = new ScrumBoard.ScrumBoard("Test Name");
            board.AddColumn("1");
            board.AddColumn("2");

            Assert.Throws<Exception>(() => board.MoveTask("name", "2"));
        }
        [Fact]
        public void DeleteTask_exist_task()
        {
            ScrumBoard.ScrumBoard board = new ScrumBoard.ScrumBoard("Test Name");
            board.AddColumn("1col");
            board.AddTask("1", "descr", Priority.low);

            board.DeleteTask("1");

            Assert.Empty(board.GetColumn(0).GetTasks());
        }
        [Fact]
        public void DeleteTask_displaced_task()
        {
            ScrumBoard.ScrumBoard board = new ScrumBoard.ScrumBoard("Test Name");
            board.AddColumn("1col");
            board.AddColumn("2col");
            board.AddTask("2", "descr", Priority.low);
            board.MoveTask("2", "2col");

            board.DeleteTask("2");

            Assert.Empty(board.GetColumn(1).GetTasks());
        }

        [Fact]
        public void DeleteTask_not_exist_task()
        {
            ScrumBoard.ScrumBoard board = new ScrumBoard.ScrumBoard("Test Name");
            board.AddColumn("1col");
            board.AddColumn("2col");
            board.AddTask("1", "descr", Priority.low);
            board.AddTask("2", "descr", Priority.low);
            board.MoveTask("2", "2col");

            Assert.Throws<Exception>(() => board.DeleteTask("name"));
        }
    }
}