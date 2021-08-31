using System.Collections.Generic;

public class UINodeRef
{
    public UINode curNode = null;
}

public class UINode
{
    public UINodeRef moveNode = null;

    public virtual UINode Enable()
    {
        return moveNode.curNode;
    }

    public virtual UINode NextNodeEnable()
    {
        return moveNode.curNode;
    }

    public virtual void Start(ref UINodeRef refData)
    {
        moveNode = refData;
    }

    public virtual void Reset()
    {
    }

    public virtual void Update()
    {
    }
}