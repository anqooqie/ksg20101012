using System;
using System.Collections.Generic;
using System.Linq;

namespace MyLibrary {
    public static class YCombinator {
        private delegate TResult FuncCalledWithMySelf<TResult>(FuncCalledWithMySelf<TResult> t);
        public static Sub Sub(Func<Sub, Sub> f) {
            return new FuncCalledWithMySelf<Sub>(g => new Sub(() => f(g(g))()))(new FuncCalledWithMySelf<Sub>(g => new Sub(() => f(g(g))())));
        }
        public static Sub<T> Sub<T>(Func<Sub<T>, Sub<T>> f) {
            return new FuncCalledWithMySelf<Sub<T>>(g => new Sub<T>(arg => f(g(g))(arg)))(new FuncCalledWithMySelf<Sub<T>>(g => new Sub<T>(arg => f(g(g))(arg))));
        }
        public static Sub<T1, T2> Sub<T1, T2>(Func<Sub<T1, T2>, Sub<T1, T2>> f) {
            return new FuncCalledWithMySelf<Sub<T1, T2>>(g => new Sub<T1, T2>((arg1, arg2) => f(g(g))(arg1, arg2)))(new FuncCalledWithMySelf<Sub<T1, T2>>(g => new Sub<T1, T2>((arg1, arg2) => f(g(g))(arg1, arg2))));
        }
        public static Sub<T1, T2, T3> Sub<T1, T2, T3>(Func<Sub<T1, T2, T3>, Sub<T1, T2, T3>> f) {
            return new FuncCalledWithMySelf<Sub<T1, T2, T3>>(g => new Sub<T1, T2, T3>((arg1, arg2, arg3) => f(g(g))(arg1, arg2, arg3)))(new FuncCalledWithMySelf<Sub<T1, T2, T3>>(g => new Sub<T1, T2, T3>((arg1, arg2, arg3) => f(g(g))(arg1, arg2, arg3))));
        }
        public static Sub<T1, T2, T3, T4> Sub<T1, T2, T3, T4>(Func<Sub<T1, T2, T3, T4>, Sub<T1, T2, T3, T4>> f) {
            return new FuncCalledWithMySelf<Sub<T1, T2, T3, T4>>(g => new Sub<T1, T2, T3, T4>((arg1, arg2, arg3, arg4) => f(g(g))(arg1, arg2, arg3, arg4)))(new FuncCalledWithMySelf<Sub<T1, T2, T3, T4>>(g => new Sub<T1, T2, T3, T4>((arg1, arg2, arg3, arg4) => f(g(g))(arg1, arg2, arg3, arg4))));
        }
        public static Func<TResult> Func<TResult>(Func<Func<TResult>, Func<TResult>> f) {
            return new FuncCalledWithMySelf<Func<TResult>>(g => new Func<TResult>(() => f(g(g))()))(new FuncCalledWithMySelf<Func<TResult>>(g => new Func<TResult>(() => f(g(g))())));
        }
        public static Func<T, TResult> Func<T, TResult>(Func<Func<T, TResult>, Func<T, TResult>> f) {
            return new FuncCalledWithMySelf<Func<T, TResult>>(g => new Func<T, TResult>(arg => f(g(g))(arg)))(new FuncCalledWithMySelf<Func<T, TResult>>(g => new Func<T, TResult>(arg => f(g(g))(arg))));
        }
        public static Func<T1, T2, TResult> Func<T1, T2, TResult>(Func<Func<T1, T2, TResult>, Func<T1, T2, TResult>> f) {
            return new FuncCalledWithMySelf<Func<T1, T2, TResult>>(g => new Func<T1, T2, TResult>((arg1, arg2) => f(g(g))(arg1, arg2)))(new FuncCalledWithMySelf<Func<T1, T2, TResult>>(g => new Func<T1, T2, TResult>((arg1, arg2) => f(g(g))(arg1, arg2))));
        }
        public static Func<T1, T2, T3, TResult> Func<T1, T2, T3, TResult>(Func<Func<T1, T2, T3, TResult>, Func<T1, T2, T3, TResult>> f) {
            return new FuncCalledWithMySelf<Func<T1, T2, T3, TResult>>(g => new Func<T1, T2, T3, TResult>((arg1, arg2, arg3) => f(g(g))(arg1, arg2, arg3)))(new FuncCalledWithMySelf<Func<T1, T2, T3, TResult>>(g => new Func<T1, T2, T3, TResult>((arg1, arg2, arg3) => f(g(g))(arg1, arg2, arg3))));
        }
        public static Func<T1, T2, T3, T4, TResult> Func<T1, T2, T3, T4, TResult>(Func<Func<T1, T2, T3, T4, TResult>, Func<T1, T2, T3, T4, TResult>> f) {
            return new FuncCalledWithMySelf<Func<T1, T2, T3, T4, TResult>>(g => new Func<T1, T2, T3, T4, TResult>((arg1, arg2, arg3, arg4) => f(g(g))(arg1, arg2, arg3, arg4)))(new FuncCalledWithMySelf<Func<T1, T2, T3, T4, TResult>>(g => new Func<T1, T2, T3, T4, TResult>((arg1, arg2, arg3, arg4) => f(g(g))(arg1, arg2, arg3, arg4))));
        }
    }
}