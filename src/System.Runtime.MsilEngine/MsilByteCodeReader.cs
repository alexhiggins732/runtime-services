using System.IO;
using System.Reflection.Emit;

namespace System.Runtime.MsilEngine
{

    /// <summary>
    /// Reads MSIL Opcodes from a byte array and provides additional services for OpCode handlers to read operand arguments.
    /// </summary>
    public class MsilByteCodeReader : IMsilReader
    {
        private byte[] il;
        private MemoryStream stream;
        private BinaryReader br;
        private int opCodeValue;
        private OpCode current;
        private IlOpCode ilOpCode;
        /// <summary>
        /// Creates a new instance of the <see cref="MsilByteCodeReader"/>
        /// </summary>
        /// <param name="il"></param>
        public MsilByteCodeReader(byte[] il)
        {
            this.il = il;
            stream = new MemoryStream(this.il);
            br = new BinaryReader(stream);
        }

        /// <summary>
        /// The current <see cref="System.Reflection.Emit.OpCode.Value"/> read from the stream
        /// </summary>
        public int OpCodeValue => opCodeValue;

        /// <summary>
        /// The current <see cref="OpCode"/> read from the stream.
        /// </summary>
        public OpCode Current => current;

        /// <summary>
        /// Attempts to read the next <see cref="System.Reflection.Emit.OpCode"/> from the stream.
        /// </summary>
        /// <returns></returns>
        public bool ReadOpCode()
        {
            opCodeValue = stream.ReadByte();
            if (opCodeValue == 254)
            {
                opCodeValue <<= 8;
                opCodeValue += stream.ReadByte();
                opCodeValue = unchecked((short)opCodeValue);
            }

            // not necessarily needed but helpful for debugging and will throw an error if an error if and invalid value was read.
            current = OpCodeLookup.OpCodes[opCodeValue];
            //ilOpCode = ILOpCodeReader.Create(opCodeValue,current, this);
            return opCodeValue == -1;
        }

        /// <summary>
        ///     Reads a Boolean value from the current stream and advances the current position
        ///     of the stream by one byte.
        /// </summary>
        /// <returns>
        ///     true if the byte is nonzero; otherwise, false.
        /// </returns>
        public virtual bool ReadBoolean() => br.ReadBoolean();

        /// <summary>
        ///     Reads the next byte from the current stream and advances the current position
        ///     of the stream by one byte.
        /// </summary>
        /// <returns>
        ///     The byte cast to a System.Int32, or -1 if the end of the stream has been reached.
        /// <returns>
        public virtual int ReadByte() => stream.ReadByte();

        /// <summary>
        ///     Reads a decimal value from the current stream and advances the current position
        ///     of the stream by sixteen bytes.
        /// </summary>
        /// <returns>
        ///     A decimal value read from the current stream.
        /// <returns>
        public virtual decimal ReadDecimal() => br.ReadDecimal();

        /// <summary>
        ///     Reads an 8-byte floating point value from the current stream and advances the
        ///     current position of the stream by eight bytes.
        /// </summary>
        /// <returns>
        ///     An 8-byte floating point value read from the current stream.
        /// <returns>
        public virtual double ReadDouble() => br.ReadDouble();

        /// <summary>
        ///     Reads a 2-byte signed integer from the current stream and advances the current
        ///     position of the stream by two bytes.
        /// </summary>
        /// <returns>
        ///     A 2-byte signed integer read from the current stream.
        /// <returns>
        public virtual short ReadInt16() => br.ReadInt16();

        /// <summary>
        ///     Reads a 4-byte signed integer from the current stream and advances the current
        ///     position of the stream by four bytes.
        /// </summary>
        /// <returns>
        ///     A 4-byte signed integer read from the current stream.
        /// <returns>
        public virtual int ReadInt32() => br.ReadInt32();

        /// <summary>
        ///     Reads an 8-byte signed integer from the current stream and advances the current
        ///     position of the stream by eight bytes.
        /// </summary>
        /// <returns>
        ///     An 8-byte signed integer read from the current stream.
        /// <returns>
        public virtual long ReadInt64() => br.ReadInt64();

        /// <summary>
        ///     Reads a signed byte from this stream and advances the current position of the
        ///     stream by one byte.
        /// </summary>
        /// <returns>
        ///     A signed byte read from the current stream.
        /// <returns>
        public virtual sbyte ReadSByte() => br.ReadSByte();

        /// <summary>
        ///     Reads a 4-byte floating point value from the current stream and advances the
        ///     current position of the stream by four bytes.
        /// </summary>
        /// <returns>
        ///     A 4-byte floating point value read from the current stream.
        /// <returns>
        public virtual float ReadSingle() => br.ReadSingle();

        /// <summary>
        ///     Reads a 2-byte unsigned integer from the current stream using little-endian encoding
        ///     and advances the position of the stream by two bytes.
        /// </summary>
        /// <returns>
        ///     A 2-byte unsigned integer read from this stream.
        /// <returns>
        public virtual ushort ReadUInt16() => br.ReadUInt16();

        /// <summary>
        ///     Reads a 4-byte unsigned integer from the current stream and advances the position
        ///     of the stream by four bytes.
        /// </summary>
        /// <returns>
        ///     A 4-byte unsigned integer read from this stream.
        /// <returns>
        public virtual uint ReadUInt32() => br.ReadUInt32();

        /// <summary>
        ///     Reads an 8-byte unsigned integer from the current stream and advances the position
        ///     of the stream by eight bytes.
        /// </summary>
        /// <returns>
        ///     An 8-byte unsigned integer read from this stream.
        /// <returns>
        public virtual ulong ReadUInt64() => br.ReadUInt64();

    }
}
