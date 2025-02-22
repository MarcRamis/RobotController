﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RobotController
{
    // This project was made by Alex Alcaide Arroyes & Marc Ramis Caldes

    public struct MyQuat
    {
        public float w;
        public float x;
        public float y;
        public float z;

        public MyQuat(float _w, float _x, float _y, float _z) { w = _w; x = _x; y = _y; z = _z; }
        
        public MyQuat Conjugate()
        {
            MyQuat q2 = new MyQuat(0,0,0,0);
            q2.x = -x;
            q2.y = -y;
            q2.z = -z;
            q2.w = w;

            return q2;
        }

        public void ToAngleAxis(out float _angle, out MyVec _axis)
        {
            _angle = 2 * (float)Math.Acos(w);
            _axis.x = x / (float)Math.Sqrt(1 - w * w);
            _axis.y = y / (float)Math.Sqrt(1 - w * w);
            _axis.z = z / (float)Math.Sqrt(1 - w * w);
        }
        public float Length()
        {
            return (float)Math.Sqrt(w * w + x * x + y * y + z * z);
        }

        public MyQuat Normalized()
        {
            return new MyQuat(w / Length(), x / Length(), y / Length(), z / Length());
        }

        public MyQuat Add(MyQuat _q1)
        {
            return new MyQuat(w + _q1.w, x + _q1.x, y + _q1.y, z + _q1.z);
        }

        public MyQuat AngleAxis(float _angle, MyVec _axis)
        {
            return new MyQuat((float)Math.Cos(_angle / 2),
                _axis.x * (float)Math.Sin(_angle / 2),
                _axis.y * (float)Math.Sin(_angle / 2),
                _axis.z * (float)Math.Sin(_angle / 2));
        }

        public MyQuat MultiplyQuat(MyQuat q1, MyQuat q2)
        {
            float w = (q1.w * q2.w) - (q1.x * q2.x) - (q1.y * q2.y) - (q1.z * q2.z);
            float x = (q1.w * q2.x) + (q1.x * q2.w) - (q1.y * q2.z) + (q1.z * q2.y);
            float y = (q1.w * q2.y) + (q1.x * q2.z) + (q1.y * q2.w) - (q1.z * q2.x);
            float z = (q1.w * q2.z) - (q1.x * q2.y) + (q1.y * q2.x) + (q1.z * q2.w);
            return new MyQuat(w, x, y, z);
        }
    }

    public struct MyVec
    {
        public float x;
        public float y;
        public float z;

        public MyVec(float _x, float _y, float _z) { x = _x; y = _y; z = _z; }
        public float Length()
        {
            return (float)Math.Sqrt(x * x + y * y + z * z);
        }

        public MyVec Normalized()
        {
            return new MyVec(x / Length(), y / Length(), z / Length());
        }
        public MyVec Cross(MyVec _vec1, MyVec _vec2)
        {
            float x, y, z;
            x = _vec1.y * _vec2.z - _vec2.y * _vec1.z;
            y = (_vec1.x * _vec2.z - _vec2.x * _vec1.z) * -1.0f;
            z = _vec1.x * _vec2.y - _vec2.x * _vec1.y;
            return new MyVec(x, y, z);
        }

        public float Dot(MyVec _vec1, MyVec _vec2)
        {
            return (_vec1.x * _vec2.x + _vec1.y * _vec2.y + _vec1.z * _vec2.z);
        }
    }






    public class MyRobotController
    {

        #region public methods

        float []angleTemp = { 0.0f, 0.0f , 60.0f , 17.3f };
        bool[] phase2 = { false, false, false, false };
        bool[] phase3 = { false, false, false, false };
        float speed = 0.2f;

        public string Hi()
        {
            
            string s = "hello world from my Robot Controller. This project has been made by Alex Alcaide Arroyes & Marc Ramis Caldes";
            return s;

        }


        //EX1: this function will place the robot in the initial position

        public void PutRobotStraight(out MyQuat rot0, out MyQuat rot1, out MyQuat rot2, out MyQuat rot3) {

            //todo: change this, use the function Rotate declared below

            for (int i = 0; i < 4; i++)
            {
                phase2[i] = false;
                phase3[i] = false;
            }
            angleTemp = new[] { 0.0f, 0.0f, 60.0f, 17.3f };

            rot0 = NullQ;
            rot1 = NullQ;
            rot2 = NullQ;
            rot3 = NullQ;

            rot0 = Rotate(rot0, new MyVec(0, 1, 0).Normalized(), Degrees2Rad(73));
            rot1 = Rotate(rot0, new MyVec(1, 0, 0).Normalized(), Degrees2Rad(0));
            rot2 = Rotate(rot1, new MyVec(1, 0, 0).Normalized(), Degrees2Rad(67));
            rot3 = Rotate(rot2, new MyVec(1, 0, 0).Normalized(), Degrees2Rad(34));

            
        }



        //EX2: this function will interpolate the rotations necessary to move the arm of the robot until its end effector collides with the target (called Stud_target)
        //it will return true until it has reached its destination. The main project is set up in such a way that when the function returns false, the object will be droped and fall following gravity.


        public bool PickStudAnim(out MyQuat rot0, out MyQuat rot1, out MyQuat rot2, out MyQuat rot3)
        {
            bool myCondition = true;
            //todo: add a check for your condition
            rot0 = NullQ;
            rot1 = NullQ;
            rot2 = NullQ;
            rot3 = NullQ;

            if (myCondition)
            {
                //todo: add your code here
                rot0 = NullQ;
                rot1 = NullQ;
                rot2 = NullQ;
                rot3 = NullQ;

                if (phase2[0] && phase2[1] && phase2[2] && phase2[3])
                {
                    if (angleTemp[0] > 40)
                    {
                        angleTemp[0] -= speed;

                    }
                    else
                    {
                        phase3[0] = true;
                    }


                    if (angleTemp[1] < 6)
                    {
                        angleTemp[1] += speed/2;

                    }
                    else
                    {
                        phase3[1] = true;
                    }

                    if (angleTemp[2] < 70)
                    {
                        angleTemp[2] += speed;

                    }
                    else
                    {
                        phase3[2] = true;
                    }

                    if (angleTemp[3] > 1)
                    {
                        angleTemp[3] -= speed;

                    }
                    else
                    {
                        phase3[3] = true;
                    }
                }
                else
                {
                    if (angleTemp[0] < 73)
                    {
                        angleTemp[0] += speed;

                    }
                    else
                    {
                        phase2[0] = true;
                    }

                    if (angleTemp[1] < 0)
                    {
                        angleTemp[1] += speed;

                    }
                    else
                    {
                        phase2[1] = true;
                    }

                    if (angleTemp[2] < 67)
                    {
                        angleTemp[2] += speed;

                    }
                    else
                    {
                        phase2[2] = true;
                    }

                    if (angleTemp[3] < 34)
                    {
                        angleTemp[3] += speed;

                    }
                    else
                    {
                        phase2[3] = true;
                    }
                }



                rot0 = Rotate(rot0, new MyVec(0, 1, 0).Normalized(), Degrees2Rad(angleTemp[0]));
                rot1 = Rotate(rot0, new MyVec(1, 0, 0).Normalized(), Degrees2Rad(angleTemp[1]));
                rot2 = Rotate(rot1, new MyVec(1, 0, 0).Normalized(), Degrees2Rad(angleTemp[2]));
                rot3 = Rotate(rot2, new MyVec(1, 0, 0).Normalized(), Degrees2Rad(angleTemp[3]));

                if(phase3[0] && phase3[1] && phase3[2] && phase3[3])
                {
                    return false;
                }

                return true;
            }

            //todo: remove this once your code works.
            
            return false;
        }


        //EX3: this function will calculate the rotations necessary to move the arm of the robot until its end effector collides with the target (called Stud_target)
        //it will return true until it has reached its destination. The main project is set up in such a way that when the function returns false, the object will be droped and fall following gravity.
        //the only difference wtih exercise 2 is that rot3 has a swing and a twist, where the swing will apply to joint3 and the twist to joint4

        public bool PickStudAnimVertical(out MyQuat rot0, out MyQuat rot1, out MyQuat rot2, out MyQuat rot3)
        {

            bool myCondition = false;
            //todo: add a check for your condition

            for (int i = 0; i < 4; i++)
            {
                phase2[i] = false;
                phase3[i] = false;
            }
            angleTemp = new[] { 0.0f, 0.0f, 60.0f, 17.3f };

            rot0 = NullQ;
            rot1 = NullQ;
            rot2 = NullQ;
            rot3 = NullQ;

            while (myCondition)
            {
                //todo: add your code here
                /*if (phase2[0] && phase2[1] && phase2[2] && phase2[3])
                {
                    if (angleTemp[0] > 40)
                    {
                        angleTemp[0] -= speed;

                    }
                    else
                    {
                        phase3[0] = true;
                    }


                    if (angleTemp[1] < 6)
                    {
                        angleTemp[1] += speed / 2;

                    }
                    else
                    {
                        phase3[1] = true;
                    }

                    if (angleTemp[2] < 70)
                    {
                        angleTemp[2] += speed;

                    }
                    else
                    {
                        phase3[2] = true;
                    }

                    if (angleTemp[3] > 1)
                    {
                        angleTemp[3] -= speed;

                    }
                    else
                    {
                        phase3[3] = true;
                    }
                }
                else
                {
                    if (angleTemp[0] < 73)
                    {
                        angleTemp[0] += speed;

                    }
                    else
                    {
                        phase2[0] = true;
                    }

                    if (angleTemp[1] < 0)
                    {
                        angleTemp[1] += speed;

                    }
                    else
                    {
                        phase2[1] = true;
                    }

                    if (angleTemp[2] < 67)
                    {
                        angleTemp[2] += speed;

                    }
                    else
                    {
                        phase2[2] = true;
                    }

                    if (angleTemp[3] < 34)
                    {
                        angleTemp[3] += speed;

                    }
                    else
                    {
                        phase2[3] = true;
                    }
                }


                rot0 = Rotate(rot0, new MyVec(0, 1, 0).Normalized(), Degrees2Rad(angleTemp[0]));
                rot1 = Rotate(rot0, new MyVec(1, 0, 0).Normalized(), Degrees2Rad(angleTemp[1]));
                rot2 = Rotate(rot1, new MyVec(1, 0, 0).Normalized(), Degrees2Rad(angleTemp[2]));
                rot3 = Rotate(rot2, new MyVec(1, 0, 0).Normalized(), Degrees2Rad(angleTemp[3]));

                if (phase3[0] && phase3[1] && phase3[2] && phase3[3])
                {
                    return false;
                }*/

                return true;

            }

            //todo: remove this once your code works.
            rot0 = NullQ;
            rot1 = NullQ;
            rot2 = NullQ;
            rot3 = NullQ;

            return false;
        }


        public static MyQuat GetSwing(MyQuat rot3)
        {
            //todo: change the return value for exercise 3
            MyQuat swing =  new MyQuat().MultiplyQuat(GetTwist(rot3).Conjugate(), rot3);
            return swing;

        }


        public static MyQuat GetTwist(MyQuat rot3)
        {
            //todo: change the return value for exercise 3
            MyQuat twist = new MyQuat(0, rot3.y, 0, rot3.w).Normalized();
            return twist;

        }




        #endregion


        #region private and internal methods

        internal int TimeSinceMidnight { get { return (DateTime.Now.Hour * 3600000) + (DateTime.Now.Minute * 60000) + (DateTime.Now.Second * 1000) + DateTime.Now.Millisecond; } }


        private static MyQuat NullQ
        {
            get
            {
                MyQuat a;
                a.w = 1;
                a.x = 0;
                a.y = 0;
                a.z = 0;
                return a;

            }
        }

        internal float Rad2Degrees(float rads)
        {
            return rads * (float)(180 / Math.PI);
        }
        internal float Degrees2Rad(float rads)
        {
            return rads * (float)(Math.PI / 180);
        }

        internal MyQuat Multiply(MyQuat q1, MyQuat q2) {

            //todo: change this so it returns a multiplication:
            float w = (q1.w * q2.w) - (q1.x * q2.x) - (q1.y * q2.y) - (q1.z * q2.z);
            float x = (q1.w * q2.x) + (q1.x * q2.w) - (q1.y * q2.z) + (q1.z * q2.y);
            float y = (q1.w * q2.y) + (q1.x * q2.z) + (q1.y * q2.w) - (q1.z * q2.x);
            float z = (q1.w * q2.z) - (q1.x * q2.y) + (q1.y * q2.x) + (q1.z * q2.w);
            return new MyQuat(w, x, y, z);

        }

        internal MyQuat Rotate(MyQuat currentRotation, MyVec axis, float angle)
        {

            //todo: change this so it takes currentRotation, and calculate a new quaternion rotated by an angle "angle" radians along the normalized axis "axis"

            MyQuat rotatorQuat = new MyQuat((float)Math.Cos(angle / 2),
                axis.x * (float)Math.Sin(angle / 2),
                axis.y * (float)Math.Sin(angle / 2),
                axis.z * (float)Math.Sin(angle / 2));

            return Multiply(rotatorQuat,currentRotation).Normalized();
        }
        


        //todo: add here all the functions needed

        


        #endregion






    }
}
