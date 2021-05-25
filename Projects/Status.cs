using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projects
{
    public enum Status { Unstarted, InProgress, Done, Overtermed }

    public class StatusCounter
    {
        private int _numOfUnstarted;
        private int _numOfInProgress;
        private int _numOfDone;
        private int _numOfOvertermed;
        public StatusCounter()
        {
            _numOfUnstarted = 0;
            _numOfInProgress = 0;
            _numOfDone = 0;
            _numOfOvertermed = 0;
        }
        public int NumOfUnstarted
        {
            get { return _numOfUnstarted; }
            set
            {
                if (value >= 0)
                    _numOfUnstarted = value;
                else
                    throw new ArgumentException("A number of tasks cannot be less than zero. Something went wrong...");
            }
        }
        public int NumOfInProgress
        {
            get { return _numOfInProgress; }
            set
            {
                if (value >= 0)
                    _numOfInProgress = value;
                else
                    throw new ArgumentException("A number of tasks cannot be less than zero. Something went wrong...");
            }
        }
        public int NumOfDone
        {
            get { return _numOfDone; }
            set
            {
                if (value >= 0)
                    _numOfDone = value;
                else
                    throw new ArgumentException("A number of tasks cannot be less than zero. Something went wrong...");
            }
        }
        public int NumOfOvertermed
        {
            get { return _numOfOvertermed; }
            set
            {
                if (value >= 0)
                    _numOfOvertermed = value;
                else
                    throw new ArgumentException("A number of tasks cannot be less than zero. Something went wrong...");
            }
        }
        public StatusCounter(StatusCounter toCopy)
        {
            NumOfUnstarted = toCopy.NumOfUnstarted;
            NumOfInProgress = toCopy.NumOfInProgress;
            NumOfDone = toCopy.NumOfDone;
            NumOfOvertermed = toCopy.NumOfOvertermed;
        }
    }
}
