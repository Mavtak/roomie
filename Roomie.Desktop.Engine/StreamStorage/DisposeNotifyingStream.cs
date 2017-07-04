﻿using System;
using System.IO;

namespace Roomie.Desktop.Engine.StreamStorage
{
    public class DisposeNotifyingStream : Stream
    {
        private Action _disposeAction;
        private bool _notifiedOfDispose;
        private Stream _stream;

        public DisposeNotifyingStream(Stream stream, Action disposeAction)
        {
            _disposeAction = disposeAction;
            _stream = stream;
        }

        public override bool CanRead => _stream.CanRead;

        public override bool CanSeek => _stream.CanSeek;

        public override bool CanWrite => _stream.CanWrite;

        public override long Length => _stream.Length;

        public override long Position
        {
            get
            {
                return _stream.Position;
            }

            set
            {
                _stream.Position = value;
            }
        }

        public override void Flush()
        {
            _stream.Flush();
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            return _stream.Read(buffer, offset, count);
        }

        public override long Seek(long offset, SeekOrigin origin)
        {
            return _stream.Seek(offset, origin);
        }

        public override void SetLength(long value)
        {
            _stream.SetLength(value);
        }

        public override void Write(byte[] buffer, int offset, int count)
        {
            _stream.Write(buffer, offset, count);
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            _stream.Dispose();

            if (!_notifiedOfDispose)
            {
                _notifiedOfDispose = true;
                _disposeAction();
            }
        }
    }
}
