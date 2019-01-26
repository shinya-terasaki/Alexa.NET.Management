﻿using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Alexa.NET.Management.Api;
using Alexa.NET.Management.UtteranceProfiler;
using Newtonsoft.Json;
using Xunit;

namespace Alexa.NET.Management.Tests
{
    public class UtteranceProfilerTests
    {
        [Fact]
        public async Task AnalyzeGeneratesCorrectUrl()
        {
            var management = new ManagementApi("xxx", new ActionHandler(req =>
            {
                Assert.Equal(HttpMethod.Post, req.Method);
                Assert.Equal("/v1/skills/skillId/stages/development/interactionModel/locales/en-GB/profileNlu", req.RequestUri.PathAndQuery);
            }, new UtteranceProfilerResponse()));
            await management.UtteranceProfiler.Analyze("skillId", SkillStage.DEVELOPMENT, "en-GB", "test");
        }

        [Fact]
        public async Task AnalyzeGeneratesCorrectBody()
        {
            var management = new ManagementApi("xxx", new ActionHandler(async req =>
            {
                var body = JsonConvert.DeserializeObject<UtteranceProfilerRequest>(
                    await req.Content.ReadAsStringAsync());
                Assert.Equal("test", body.Utterance);
                Assert.Equal("test2", body.MultiTurnToken);
                return new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent(JsonConvert.SerializeObject(new UtteranceProfilerResponse()))
                };
            }));
            await management.UtteranceProfiler.Analyze("skillId", SkillStage.DEVELOPMENT, "en-GB", "test", "test2");
        }

        [Fact]
        public void ResponseIsCorrect()
        {
            var response = GetFromFile<UtteranceProfilerResponse>("Examples/UtteranceProfilerResponse.json");

        }

        public JsonSerializer Serializer = JsonSerializer.Create(new JsonSerializerSettings());

        private T GetFromFile<T>(string path)
        {
            using (var reader = new JsonTextReader(File.OpenText(path)))
            {
                return Serializer.Deserialize<T>(reader);
            }
        }
    }
}
