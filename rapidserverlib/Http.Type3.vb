Namespace Http
    Namespace Type3
        ''' <summary>
        ''' An http server with an async I/O model implemented via SocketAsyncEventArgs (.NET 3.5+). Utilizes a special async model designed for the Socket class which consists of a shared buffer and pre-allocated object pool for async state objects to avoid object instantiation and memory thrashing/fragmentation during every http request.
        ''' The MSDN code example for this pattern is very poor. The issues (and solution) are explained in the Background section in this tutorial: http://www.codeproject.com/Articles/83102/C-SocketAsyncEventArgs-High-Performance-Socket-Cod?fid=1573061
        ''' </summary>
        ''' <remarks></remarks>
        Public Class Server

        End Class
    End Namespace
End Namespace
