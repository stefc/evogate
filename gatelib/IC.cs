using System;

namespace stefc.gatelib;

public class IC {

    private readonly string name; 

    private readonly int input; 

    private readonly int output; 

    private readonly int gates;

    private readonly  Graph<Gate> wiring;


    public IC (string name, int input, int output, int gates, Graph<Gate> wiring)
    {
        if(input<=0) throw new ArgumentException("input");
        if(output<=0) throw new ArgumentException("output");
        if(gates<=0) throw new ArgumentException("gates");
        
        this.input=input;
        this.output=output;
        this.gates=gates;
        this.name=name;
        this.wiring=wiring;
    }

    public string Name => name;
    public int Input => input; 
    public int Output => output;
    public int Gates => gates;
    public Graph<Gate> Wiring => wiring;
}

public class Gate : IEquatable<Gate> {

    private readonly string id;

    private readonly GateOp op;
    public Gate (string id, GateOp op)
    {
        if(String.IsNullOrEmpty(id)) throw new ArgumentException("id");

        this.op=op;
        this.id=id;
    }

    public string Id => id;
    public GateOp Op => op;

    public bool Equals(Gate other)
    {
        return this.id.Equals(other.id);
    }
}