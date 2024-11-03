using System;
using System.Collections.Generic;
using System.Text;

namespace SkyTrackerLib
{
    
    public class FlightList
    {
        int num = 0; // numero de vols en la llista
        List<FlightPlan> listFligths = new List<FlightPlan>(100); // canviar el 100 pel num de elements en el datagrid
        
        // Methods
        public int GetNum()
        {
            // Return the number of added flights plans on
            // the list
            return num;
        }
        public int AddFlightPlan(FlightPlan f)
        {
            // Recibes a FlightPlan and is added to the list
            // If everything goes ok return a 0
            // If the list is full it retuns a -1
            // Else it retuns a 1 indicating that
            // something didn't go as expected
            if (num == 100) { return -1; }
            else
            {
                listFligths.Add(f);
                num++;
                return 0;
            }
        }
        public FlightPlan GetFlightPlan(int i)
        {
            // Obtains the flight in the specified position
            // out of the list of flight plans
            if (i < 0 || i >= num) { return null; }
            else { return listFligths[i]; }
        }
        public void MoveForward(int time)
        {
            // Loops through all the elements of the vector
            // and moves them all forward.
            for (int i = 0; i < num; i++)
                listFligths[i].MoveForwardFlightPlan(time);
        }
        public void MoveBackward()
        {
            // Loops through all the elements of the vector
            // and moves them all backward.
            for (int i = 0; i < num; i++)
                listFligths[i].MoveBackwardFlightPlan();
        }
        public void ClearList() {
            //Delete all the elements contained in our vector.
            listFligths.Clear(); 
            num = 0; 
        }
        public bool Delete(string name)
        {
            //Find an element in our flight plan vector and
            //delete it.
            bool find = false;
            for (int i = 0;i < num; i++)
            {
                if (listFligths[i].GetTargetId() == name)
                {
                    listFligths.RemoveAt(i);
                    find = true;
                    num--;
                }
            }
            return find;
        }
    }
}
