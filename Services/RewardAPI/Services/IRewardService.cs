using OrderNow.Services.RewardAPI.Message;

namespace OrderNow.Services.RewardAPI.Services
{
    public interface IRewardService
    {
        Task UpdateRewards(RewardsMessage rewardsMessage);
    }
}
