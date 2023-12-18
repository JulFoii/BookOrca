using System;
using System.Runtime.CompilerServices;
using BookOrca.ApiAccess;
using BookOrca.DataAccess;

namespace BookOrca.Core;

public static class Singletons
{
    private static IBookDataAccess bookDataAccess = null!;
    private static IBookApi bookApi = null!;

    public static IBookDataAccess BookDataAccess
    {
        get => bookDataAccess;
        set
        {
            if (bookDataAccess != null) throw new Exception(GetErrorMessage());
            bookDataAccess = value;
        }
    }

    public static IBookApi BookApi
    {
        get => bookApi;
        set
        {
            if (bookDataAccess != null) throw new Exception(GetErrorMessage());

            bookApi = value;
        }
    }

    private static string GetErrorMessage([CallerMemberName] string? memberName = null)
    {
        if (memberName == null) throw new ArgumentNullException(memberName);

        return $"{memberName} is already set to an instance.";
    }
}