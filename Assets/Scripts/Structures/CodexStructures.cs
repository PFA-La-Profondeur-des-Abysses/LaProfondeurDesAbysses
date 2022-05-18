
using System.Collections.Generic;

public class Codex
{
    public List<Rapport> rapports;
}

public class Rapport
{
    public List<Form> forms;
}

public class Form
{
    public List<Option> options;
}

public class Option
{
    public string button;
    public string text;
}