using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkyTrackerLib
{
    public class FlightPlan
    {
        // Atributes
        int time;
        string targetAddress, targetId;
        float heading, latitud, lonitud, h, rho, theta, flight_level, mach;
        // Postions
        Position InitialPosition;   // initial position
        Position CurrentPosition;   // actual postion


        // Not data to be used for determining the position
        // but needed for extra functions
        float tas, ias, gs;

        Stack<Position> positions = new Stack<Position>();

        // Struct
        public FlightPlan(int time, string targetAddr, string targetId, float lat, float lon, float fl, float m)
        {
            this.time = time;
            this.targetAddress = targetAddr;
            this.targetId = targetId;
            this.latitud = lat;
            this.lonitud = lon;
            this.flight_level = fl;
            this.mach = m;

            this.InitialPosition = new Position(lat, lon);
            this.CurrentPosition = new Position(lat, lon);
        }

        // Methods
        // Gets
        public string GetTargetAddres() { 
            // Get Addres of the flight plan
            return this.targetAddress; 
        }
        public string GetTargetId() { 
            // Get the id of the flight plan
            return this.targetId; 
        }
        public float GetLatitud() {
            // Get the latitude of the flight plan
            return this.latitud;
        }
        public float GetLonitud() {
            // Get the longitude of the flight plan
            return this.lonitud;
        }
        public float GetMach() {
            // Get the mach of the flight plan
            return this.mach;
        }
        public Position GetInitialPostion() {
            // Get the inital postion of the plane
            // in order to enable resets
            return this.InitialPosition;
        }
        public Position GetCurrentPostion() {
            // Get the curent postion of the plane
            return this.CurrentPosition;
        }
        public Stack<Position> GetPositions() {
            // Get all posicions of the plane
            return this.positions;
        }

        // Sets
        public void SetInitialPosition(Position position) {
            // Sets the inital position
            this.InitialPosition = position;
        }
        public void SetCurrentPosition(Position position) {
            // Sets the current position
            this.CurrentPosition = position;
        }
        public void SetPositions(Stack<Position> positions) {
            // Sets all postions
            this.positions = positions;
        }

        // Functions
        public void AddPostion(float x, float y)
        {
            Position postion = new Position(x, y);
            positions.Push(postion);
        }
        // TODO: afegir funcions Add de les dades que volguem
        // tenir desades
        public void MoveForwardFlightPlan(int t)
        {
            // Check if there is actual values for the lat
            // and lon at time t. If there is time current
            // postion is updates. If not the current
            // remains unchanged.
            for (int i = 0; i < t; i++)
            {
                if (this.time <= t)
                {
                    this.time = t;

                } 
            }
        }
        public void MoveBackwardFlightPlan()
        {
            // Moves one position in the opposite direction to
            // the aircraft movement.
            if (this.CurrentPosition != this.InitialPosition)
            {
                // The Pop function allows us to undo positions
                // by removing them from the stack.
                positions.Pop();
                //Puts the new currentPosition at the top of the
                //stack.
                this.CurrentPosition = this.positions.Peek();
            }
            else
            {
                positions.Clear();
            }
        }
        public void Reset()
        {
            // Return the cuurent position to the starting point
            this.CurrentPosition = InitialPosition;
        }
    }
}
