namespace S20L.lib;
public class  Person
{

    public string person_first_name{get;set;}
    public string person_last_name{get;set;}
    public string person_state{get;set;}
    public string person_email{get;set;}
    public string person_number{get;set;}
    public string person_relationship{get;set;}
    public Person(string person_first_name,string person_last_name, string person_number, string person_email, string person_state, string person_relationship)
    {
        this.person_first_name = person_first_name;
        this.person_last_name = person_last_name;
        this.person_number = person_number;
        this.person_email = person_email;
        this.person_state = person_state;
        this.person_relationship = person_relationship;
    }

    public override string ToString()
    {
        return $"{this.person_first_name},{this.person_last_name},{this.person_number},{this.person_email},{this.person_state},{this.person_relationship}";
    }
    public static Person new_person(string p)
    {
        var toks = p.Split(",");
        string fname = toks[0];
        string lname = toks[1];
        string number = toks[2];
        string email = toks[3];
        string state = toks[4];
        string relationship = toks[5];

        Person newP = new Person(fname,lname,number,email,state,relationship);
        return newP;
    }
}