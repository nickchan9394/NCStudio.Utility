using Microsoft.EntityFrameworkCore;
using Renci.SshNet;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace NCStudio.Utility.Testing
{
    public abstract class DockerStartupFixture : IDisposable
    {
        protected string dockerHost;
        protected string dockerUser;
        protected string dockerPassword;


        public DockerStartupFixture()
        {
            try
            {
                readDockerVariablesFromEnv();

                InitTestEnv();
            }
            catch (Exception e)
            {
                this.Dispose();
                throw e;
            }
        }

        protected abstract void InitTestEnv();

        public string GetApi(string path,string port)
        {
            return $"http://{dockerHost}:{port}/{path}";
        }

        public string GetHost()
        {
            return dockerHost;
        }

        public void ActOnDb<T>(DbContextOptions dbOptions,Action<T> act) where T : IDisposable
        {
            using (var context = (T)Activator.CreateInstance(typeof(T),dbOptions))
            {
                act(context);
            }
        }

        private void readDockerVariablesFromEnv()
        {
            dockerHost = Environment.GetEnvironmentVariable("INTEGRATIONTEST_DOCKERHOST");
            dockerUser = Environment.GetEnvironmentVariable("INTEGRATIONTEST_DOCKERUSER");
            dockerPassword = Environment.GetEnvironmentVariable("INTEGRATIONTEST_DOCKERPASSWORD");
        }


        public abstract void Dispose();

        protected void RunSSHCommand(string commandText)
        {
            using (var sshClient = new SshClient(dockerHost, dockerUser, dockerPassword))
            {
                sshClient.Connect();
                var result = sshClient.RunCommand(commandText);
                sshClient.Disconnect();
            }
        }

        protected void RunProcess(string fileName, string arguments)
        {
            var process = Process.Start(new ProcessStartInfo
            {
                FileName = fileName,
                Arguments = arguments
            });

            process.WaitForExit();
            if (process.ExitCode != 0) throw new Exception($"Process:{fileName} exit due to unexpected error, exit-code:{process.ExitCode}");
        }

        protected async Task<bool> WaitForService(string uri,int timeoutSeconds=60)
        {
            using (var client = new HttpClient() { Timeout = TimeSpan.FromSeconds(1) })
            {
                var startTime = DateTime.Now;
                while (DateTime.Now - startTime < TimeSpan.FromSeconds(timeoutSeconds))
                {
                    try
                    {
                        var response = await client.GetAsync(uri).ConfigureAwait(false);
                        if (response.IsSuccessStatusCode)
                        {
                            return true;
                        }
                    }
                    catch
                    {
                        // Ignore exceptions, just retry
                    }

                    await Task.Delay(1000).ConfigureAwait(false);
                }
            }

            return false;
        }

    }
}
