using System;

namespace LibrarySystem
{
    public interface ISerializable
    {
        string Serialize();
        void Deserialize(string data);
    }
} 