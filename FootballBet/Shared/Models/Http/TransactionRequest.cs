namespace FootballBet.Shared.Models.Http;

public readonly record struct TransactionRequest
(
    decimal DepositAmount,
    decimal WithdrawAmount
);