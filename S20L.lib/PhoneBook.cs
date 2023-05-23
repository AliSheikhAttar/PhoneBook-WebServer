using System.Text;
namespace S20L.lib;

public class PhoneBook
{
    public List<Person> _Contacts;
    public void Add_to_phonebook(Person o)
    {
        _Contacts.Add(o);

    }
    public PhoneBook()
    {
        _Contacts = new List<Person>();
    }

    public List<Person> GetAllContacts()
    {
        return _Contacts;
    }
    public string add_contact(Person p)
    {
        _Contacts.Add(p);

        return "contact added";
    }

    public string GetContactinfobyname(string name, string item)
    {
        foreach(var p in _Contacts)
        {
            if (p.person_first_name == name)
            {
                if(item=="number")
                {
                    return $"{p.person_first_name} s' number is :{p.person_number}";
                }
                else if(item == "email")
                {
                    return $"{p.person_first_name} s' email is :{p.person_email}";
                }
                else if(item == "state")
                {
                    return $"{p.person_first_name} s' state is :{p.person_state}";
                }
                else if(item== "relationship")
                {
                    return $"{p.person_first_name} s' relationship is :{p.person_relationship}";
                }
                else if(item== "name")
                {
                    return $"{p.person_first_name} s' full name is :{p.person_first_name},{p.person_last_name}";
                }
                else
                    return $"{item} is not recorded for contacts!";
            }
        }
        return $"no contact was found with the name {name} !";
    }

    public string GetContactinfobynumber(string number, string item)
    {

        foreach(var p in _Contacts)
        {
            if (p.person_number == number)
            {
                if(item=="name")
                {
                    return $"{number} s' registered name is :{p.person_first_name},{p.person_last_name}";
                }
                else if(item == "email")
                {
                    return $"{number} s' registered email is :{p.person_email}";
                }
                else if(item== "state")
                {
                    return $"{number} s' registered state is :{p.person_state}";
                }
                else if(item== "relationship")
                {
                    return $"{number} s' registered relationship is :{p.person_relationship}";
                }
                else if(item== "number")
                {
                    return $"you entered the number!";
                }
                else
                    return $"{item} is not recorded for contacts!";
            }
        }
        return $"no contact was found with the number {number} !";
    }


    public void Save(string Address)
    {

        string fr = File.ReadAllText(Address);
        string ey = string.Empty;
        foreach(var p in this._Contacts)
            if(!fr.Contains($"{p}"))
            {
                ey = $"{p}\n";
                File.AppendAllText(Address,ey);
                fr = File.ReadAllText(Address);
            }
        
    }
        public bool Search(string firstName, string lastName,int? number, out Person s)
        {
            s = null;
            foreach(var c in this._Contacts)
            {
                if ( (string.IsNullOrWhiteSpace(firstName) || c.person_first_name.StartsWith(firstName)) &&
                    (string.IsNullOrWhiteSpace(lastName) || c.person_last_name.StartsWith(lastName)) &&
                    (! number.HasValue || number.Value == int.Parse(c.person_number) ) )
                    {
                        s = c;
                        return true;
                    }
            }
            return false;
        }

    public bool Delete_contact(string number)
    {

        for (int i =0;i<_Contacts.Count;i+=1)
        {
            if(_Contacts[i].person_number == number)
            {
                remove_the_nth_contact_from_phonebook(i);
                return true;
            }
        }
        return false;
    }
    public void Edit_contact(Person n)
    {
        foreach(var p in _Contacts)
        {
            if(p.person_number == n.person_number || p.person_first_name == n.person_first_name || p.person_email == n.person_email)
            {
                p.person_first_name = n.person_first_name;
                p.person_last_name = n.person_last_name;
                p.person_number = n.person_number;
                p.person_email = n.person_email;
                p.person_state = n.person_state;
                p.person_relationship = n.person_relationship;
            }

        }
    }
    public void remove_the_nth_contact_from_phonebook(int i)
    {
            if(_Contacts.LongCount() != 0)
            {
                _Contacts.RemoveAt(i);
            }
            else
                Console.WriteLine("phonebook is empty");

    }

    public Person get_nth_contact(int i)
    {
        return _Contacts[i-1];
    }
    public List<Person> Contacts()
    {
        return _Contacts;
    }

}