using Blaze3SDK.Blaze.Redirector;
using Blaze3SDK.Components;
using BlazeCommon;

namespace TheDirector;

internal class RedirectorComponent : RedirectorComponentBase.Server
{

    public override Task<ServerInstanceInfo> GetServerInstanceAsync(ServerInstanceRequest request, BlazeRpcContext context)
    {

        var responseData = new ServerInstanceInfo
        {
            mAddress = new ServerAddress
            {
                IpAddress = new IpAddress
                {
                    mHostname = Program.PublicIp,
                    mIp = Util.GetIPAddressAsUInt(Program.PublicIp),
                },
            },
            mMessages = new List<string>
            {
                { "Our servers have moved!\nPlease update your RPCS3 config with:\ngosredirector.ea.com==57.131.155.88" }
            },
            mSecure = false,
        };
        
        switch (request.mClientName)
        {
            case "NHL10":
            {
                responseData.mAddress.IpAddress = responseData.mAddress.IpAddress.Value with { mPort = 13337 };
                break;
            }
            case "NHL11":
            {
                responseData.mAddress.IpAddress = responseData.mAddress.IpAddress.Value with { mPort = 13367 };
                break;
            }
            case "NHL12":
            {
                responseData.mAddress.IpAddress = responseData.mAddress.IpAddress.Value with { mPort = 26767 };
                break;
            }
            case "NHL13":
            {
                responseData.mAddress.IpAddress = responseData.mAddress.IpAddress.Value with { mPort = 36767 };
                break;
            }
            case "NHL14":
            {
                responseData.mAddress.IpAddress = responseData.mAddress.IpAddress.Value with { mPort = 34767 };
                break;
            }
            case "NHL15":
            {
                responseData.mAddress.IpAddress = responseData.mAddress.IpAddress.Value with { mPort = 16567 };
                break;
            }
            case "NHL16":
            {
                responseData.mAddress.IpAddress = responseData.mAddress.IpAddress.Value with { mPort = 16767 };
                break;
            }
            
        }

        return Task.FromResult(responseData);
    }
}