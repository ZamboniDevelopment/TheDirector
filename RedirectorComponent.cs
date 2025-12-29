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
                {"You are now connecting to a private server Zamboni\nNot affiliated with EA Sports"}
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
                if (request.mBlazeSDKBuildDate.Equals("Sep 21 2010 18:00:56") || request.mBlazeSDKBuildDate.Equals("Sep 21 2010 18:01:00"))
                {
                    responseData.mMessages = new List<string>
                    {
                        { "Playing with a title patch (versions 01.01 or 01.02) is not currently supported by this server.\nPlease downgrade your game to play online" }
                    };
                    break;
                }
                responseData.mAddress.IpAddress = responseData.mAddress.IpAddress.Value with { mPort = 13367 };
                break;
            }
            case "NHL14":
            {
                responseData.mAddress.IpAddress = responseData.mAddress.IpAddress.Value with { mPort = 34767 };
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