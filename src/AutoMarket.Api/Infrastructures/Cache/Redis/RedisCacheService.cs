﻿using System;
using System.Text.Json;
using System.Threading.Tasks;

namespace AutoMarket.Api.Infrastructures.Cache.Redis
{
    public class RedisCacheService : ICacheService
    {
        private RedisServer _redisServer;

        public RedisCacheService(RedisServer redisServer)
        {
            _redisServer = redisServer;
        }

        public async Task Add(string key, object data, TimeSpan? expireTime = null)
        {
            string jsonData = JsonSerializer.Serialize(data);
            await _redisServer.Database.StringSetAsync(key, jsonData, expireTime);
        }

        public async Task<bool> Any(string key)
        {
            return await _redisServer.Database.KeyExistsAsync(key);
        }

        public async Task<T> Get<T>(string key)
        {
            if (await Any(key))
            {
                string jsonData = await _redisServer.Database.StringGetAsync(key);
                return JsonSerializer.Deserialize<T>(jsonData);
            }

            return default;
        }

        public async Task Remove(string key)
        {
            await _redisServer.Database.KeyDeleteAsync(key);
        }

        public void Clear()
        {
            _redisServer.FlushDatabase();
        }

        public async Task<bool> KeyExists(string key)
        {
            return await _redisServer.Database.KeyExistsAsync(key);
        }
    }
}
