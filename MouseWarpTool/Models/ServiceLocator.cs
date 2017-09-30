using System;
using System.Collections.Generic;
using System.Linq;

namespace MouseWarpTool.Models
{
    class ServiceLocator
    {
        public static ServiceLocator Instance { get; } = new ServiceLocator();

        //インスタンスを破棄しないシングルトンスコープ
        private readonly InstanceContext applicationContext = new InstanceContext();
        
        private ServiceLocator()
        {
        }

        /// <summary>
        /// ServiceLocatorの初期化
        /// </summary>
        public void InitializeServiceLocator()
        {
            RegistFactories();
            InitializeContexts();
        }

        private void RegistFactories()
        {
            RegisterByApplicationScope<ConfigRepository>(() => new ConfigRepository());
        }

        private void InitializeContexts()
        {
            //インスタンスを最初に用意
            applicationContext.Initialize();
            //全てのコンテキストの用意が終わってから呼び出し
            applicationContext.CallInitializeAll();
        }

        /// <summary>
        /// インスタンスを取得
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T GetInstance<T>()
        {
            var instance = applicationContext.GetInstance<T>();
            if (instance != null)
            {
                return instance;
            }
            throw new NullReferenceException("Instance not found.");
        }

        private void RegisterByApplicationScope<T>(Func<T> instanceFactory) where T : class
        {
            applicationContext.RegisterFactory<T>(instanceFactory);
        }
    }

    /// <summary>
    /// インスタンスの生存管理を行うコンテキスト
    /// </summary>
    public class InstanceContext
    {
        //インスタンスを作成するファクトリ
        protected readonly Dictionary<Type, Func<object>> Factory = new Dictionary<Type, Func<object>>();
        //作成したインスタンスのキャッシュ
        private readonly Dictionary<Type, object> _instance = new Dictionary<Type, object>();
        
        /// <summary>
        /// 型からインスタンスを返します。<br/>
        /// キャッシュインスタンスか新しいインスタンスを返すかはInstanceContextのスコープに依存します。
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public virtual T GetInstance<T>()
        {
            var type = typeof(T);

            //インスタンスキャッシュから探す
            object o;
            if (_instance.TryGetValue(type, out o))
            {
                return (T)o;
            }

            //見つからないのでファクトリを探す
            Func<object> func;
            if (!Factory.TryGetValue(type, out func))
            {
                return default(T);
            }
            //見つかればファクトリからインスタンスを初期化
            o = func?.Invoke();
            if (o is IInitializable)
            {
                var initializable = (IInitializable) o;
                initializable.Initialize();
            }

            _instance.Add(type, o);
            return (T) o;
        }

        /// <summary>
        /// 全てのインスタンスを作成し、インスタンスキャッシュに詰める
        /// </summary>
        public virtual void Initialize()
        {
            ClearCacheAll();
            foreach (var keyValuePair in Factory)
            {
                _instance.Add(keyValuePair.Key, keyValuePair.Value.Invoke());
            }
        }

        /// <summary>
        /// 初期化メソッドを呼び出す
        /// </summary>
        public virtual void CallInitializeAll()
        {
            foreach (var value in _instance.Values)
            {
                (value as IInitializable)?.Initialize();
            }
        }

        /// <summary>
        /// 全てのインスタンスキャッシュを削除します。
        /// </summary>
        public void ClearCacheAll()
        {
            //インスタンスの後処理
            foreach (var disposable in _instance.Values.OfType<IDisposable>())
            {
                disposable.Dispose();
            }
            _instance.Clear();
        }

        /// <summary>
        /// 指定の型のインスタンスキャッシュを削除します。
        /// </summary>
        /// <param name="type"></param>
        public void ClearChacheByType(Type type)
        {
            _instance.Remove(type);
        }

        public void RegisterFactory<T>(Func<T> instanceFactory) where T : class
        {
            Factory.Add(typeof(T), instanceFactory);
        }
    }

    interface IInitializable
    {
        void Initialize();
    }
}
