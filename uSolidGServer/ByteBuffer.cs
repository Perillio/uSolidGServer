using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace uSolidGServer
{
    public class ByteBuffer : IDisposable 
    {
        private List<byte> Buff;
        private Byte[] readBuff;
        private int readPos;
        private bool buffUpdated = false;

        public ByteBuffer() //Resets everything to 0
        {
            Buff = new List<byte>();
            readPos = 0;
        }
        public int GetReadPos() //Shows the position in the buffer
        {
            return readPos;
        }
        public Byte[] ToArray() //Bufferdata as Array
        {
            return Buff.ToArray();
        }
        public int Count() //The Count of the buffer
        {
            return Buff.Count();
        }
        public int Lenght() //How long is the rest of the Array ?
        {
            return Count() - readPos;
        }
        public void Clear() //Clears the buffer
        {
            Buff.Clear();
            readPos = 0;
        }

        public void WriteByte(byte input)
        {
            Buff.Add(input);
            buffUpdated = true;
        }
        public void WriteBytes(byte[] input)
        {
            Buff.AddRange(input);
            buffUpdated = true;
        }
        public void WriteShort(short input)
        {
            Buff.AddRange(BitConverter.GetBytes(input));
            buffUpdated = true;
        }
        public void WriteInteger(int input)
        {
            Buff.AddRange(BitConverter.GetBytes(input));
            buffUpdated = true;
        }
        public void WriteLong(long input)
        {
            Buff.AddRange(BitConverter.GetBytes(input));
            buffUpdated = true;
        }
        public void WriteFloat(float input)
        {
            Buff.AddRange(BitConverter.GetBytes(input));
            buffUpdated = true;
        }
        public void WriteBool(bool input)
        {
            Buff.AddRange(BitConverter.GetBytes(input));
            buffUpdated = true;
        }
        public void WriteString(string input)
        {
            Buff.AddRange(BitConverter.GetBytes(input.Length));
            Buff.AddRange(Encoding.ASCII.GetBytes(input));
            buffUpdated = true;
        }

        public byte ReadByte(bool peek = true)
        {
            if(Buff.Count > readPos)
            {
                if(buffUpdated)
                {
                    readBuff = Buff.ToArray();
                    buffUpdated = false;
                }

                byte value = readBuff[readPos];
                if(peek & Buff.Count > readPos)
                {
                    readPos += 1;
                }

                return value;
            }
            else
            {
                throw new Exception("Value is not a type of 'byte'");
            }
        }
        public byte[] ReadBytes(int Length, bool peek = true)
        {
            if (Buff.Count > readPos)
            {
                if (buffUpdated)
                {
                    readBuff = Buff.ToArray();
                    buffUpdated = false;
                }

                byte[] value = Buff.GetRange(readPos, Length).ToArray();
                if (peek)
                {
                    readPos += Length;
                }

                return value;
            }
            else
            {
                throw new Exception("Value is not a type of 'byte-array'");
            }
        }
        public short ReadShort(bool peek = true)
        {
            if (Buff.Count > readPos)
            {
                if (buffUpdated)
                {
                    readBuff = Buff.ToArray();
                    buffUpdated = false;
                }

                short value = BitConverter.ToInt16(readBuff,readPos);
                if (peek & Buff.Count > readPos)
                {
                    readPos += 2;
                }

                return value;
            }
            else
            {
                throw new Exception("Value is not a type of 'short'");
            }

        }
        public int ReadInteger(bool peek = true)
        {
            if (Buff.Count > readPos)
            {
                if (buffUpdated)
                {
                    readBuff = Buff.ToArray();
                    buffUpdated = false;
                }

                int value = BitConverter.ToInt32(readBuff, readPos);
                if (peek & Buff.Count > readPos)
                {
                    readPos += 4;
                }

                return value;
            }
            else
            {
                throw new Exception("Value is not a type of 'integer'");
            }
        }
        public long ReadLong(bool peek = true)
        {
            if (Buff.Count > readPos)
            {
                if (buffUpdated)
                {
                    readBuff = Buff.ToArray();
                    buffUpdated = false;
                }

                long value = BitConverter.ToInt64(readBuff, readPos);
                if (peek & Buff.Count > readPos)
                {
                    readPos += 8;
                }

                return value;
            }
            else
            {
                throw new Exception("Value is not a type of 'long'");
            }
        }
        public float ReadFloat(bool peek = true)
        {
            if (Buff.Count > readPos)
            {
                if (buffUpdated)
                {
                    readBuff = Buff.ToArray();
                    buffUpdated = false;
                }

                float value = BitConverter.ToSingle(readBuff, readPos);
                if (peek & Buff.Count > readPos)
                {
                    readPos += 4;
                }

                return value;
            }
            else
            {
                throw new Exception("Value is not a type of 'float'");
            }
        }
        public bool ReadBool(bool peek = true)
        {
            if (Buff.Count > readPos)
            {
                if (buffUpdated)
                {
                    readBuff = Buff.ToArray();
                    buffUpdated = false;
                }

                bool value = BitConverter.ToBoolean(readBuff, readPos);
                if (peek & Buff.Count > readPos)
                {
                    readPos += 1;
                }

                return value;
            }
            else
            {
                throw new Exception("Value is not a type of 'bool'");
            }
        }
        public string ReadString(bool peek = true)
        {
            try
            {
                int Length = ReadInteger(true);
                if (buffUpdated)
                {
                    readBuff = Buff.ToArray();
                    buffUpdated = false;
                }
                string value = Encoding.ASCII.GetString(readBuff, readPos, Length);
                if (peek & Buff.Count > readPos)
                {
                    if (value.Length > 0)
                        readPos += Length;
                }

                return value;
            }
            catch(Exception)
            {

                throw new Exception("Value is not a type of 'string'");
            }
            
        }

        private bool disposedValue = false;
        protected virtual void Dispose(bool disposing)
        {
            if (!disposing)
            {
                if(disposing)
                {
                    Buff.Clear();
                    readPos = 0;
                }
                disposedValue = true;
            }
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
