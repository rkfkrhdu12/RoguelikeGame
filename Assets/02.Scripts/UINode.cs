using System.Collections.Generic;

public class UINodeRef
{
    public UINode curNode = null;
}

public class UINode
{
    public UINodeRef nextNode = null;
    public UINode prevNode = null;

    public virtual UINode Valid()
    {
        return nextNode.curNode;
    }

    public virtual UINode Invalid()
    {
        return prevNode;
    }

    public virtual void Start(ref UINodeRef refData)
    {
        nextNode = refData;
    }

    public virtual void Reset()
    {

    }

    public virtual void Update()
    {

    }
}