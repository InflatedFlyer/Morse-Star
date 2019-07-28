using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Operate
{
    public enum Command
    {
        None,
        Walk,
        Run,
        Trigger,
        SaveScene,
        VirtualMove,
        VirtualTrigger
    }

    private static Queue<Command> cmd_queue=new Queue<Command>();
    private static Queue<float> axis_queue = new Queue<float>();
    private static Queue<Vector2> mouse_queue = new Queue<Vector2>();
    public static void AddCommand(Command cmd,float a=0)
    {
        cmd_queue.Enqueue(cmd);
        if (a != 0)
        {
            axis_queue.Enqueue(a);
        }
    }
    public static void AddCommand(Command cmd,Vector2 mousePos)
    {
        cmd_queue.Enqueue(cmd);
        mouse_queue.Enqueue(mousePos);
    }

    public static Command GetCommand()
    {
        if (cmd_queue.Count == 0)
        {
            return Command.None;
        }
        else
        {
            return cmd_queue.Dequeue();
        }
    }
    public static float GetAxisHorizon()
    {
        if(axis_queue.Count==0)
        {
            return 0;
        }
        else
        {
            return axis_queue.Dequeue();
        }
    }
    public static bool GetMousePosition(out Vector2 mouse_pos)
    {
        if (mouse_queue.Count == 0)
        {
            mouse_pos=Vector2.zero;
            return false;
        }
        else
        {
            mouse_pos=mouse_queue.Dequeue();
            return true;
        }
    }

    public static void ClearCommand()
    {
        cmd_queue.Clear();
        axis_queue.Clear();
        mouse_queue.Clear();
    }
}
