namespace CodeBase.Common.Web.Optimization.Compression {
    using System;
    using System.IO;
    using System.Text;
    using System.Text.RegularExpressions;

    using Utilities.Compression;

    public class CompressorStream : Stream {
        private readonly string _compression;
        private readonly Stream _stream;
        private string _finalString;

        public CompressorStream(Stream stream,
                                string compression) {
            _compression = compression;
            _stream = stream;
        }

        public override bool CanRead {
            get { return false; }
        }

        public override bool CanSeek {
            get { return false; }
        }

        public override bool CanWrite {
            get { return true; }
        }

        public override void Flush() {
            if (string.IsNullOrEmpty(_finalString))
                return;

            _finalString = Regex.Replace(_finalString,
                                         "/// <.+>",
                                         "");
            _finalString = Regex.Replace(_finalString,
                                         @">[\s\S]*?<",
                                         Evaluate);

            var data = Encoding.UTF8.GetBytes(_finalString);

            if (_compression.Equals("deflate"))
                data = Deflate.Compress(data);
            else
                data = GZip.Compress(data);

            _stream.Write(data,
                          0,
                          data.Length);

            _finalString = "";
        }

        public override long Length {
            get { throw new NotImplementedException(); }
        }

        public override long Position {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }

        public override int Read(byte[] buffer,
                                 int offset,
                                 int count) {
            throw new NotImplementedException();
        }

        public override long Seek(long offset,
                                  SeekOrigin origin) {
            throw new NotImplementedException();
        }

        public override void SetLength(long value) {
            throw new NotImplementedException();
        }

        public override void Write(byte[] buffer,
                                   int offset,
                                   int count) {
            var data = new byte[count];

            Buffer.BlockCopy(buffer,
                             offset,
                             data,
                             0,
                             count);

            var input = Encoding.UTF8.GetString(data);

            _finalString += input;
        }

        protected string Evaluate(Match matcher) {
            var matchedString = matcher.ToString();

            matchedString = Regex.Replace(matchedString,
                                          @"\r\n\s*",
                                          "");
            matchedString = Regex.Replace(matchedString,
                                          @"\n\s*",
                                          "");

            return matchedString;
        }
    }
}