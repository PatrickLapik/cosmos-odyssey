import { useValidTimer } from "@/providers/ValidTimerProvider";

export const PriceListValidTimer = () => {
    const { timeLeft } = useValidTimer();

    if (!timeLeft) return null;

  return (
    <TimerText
      minutes={timeLeft.minutes}
      seconds={timeLeft.seconds}
      total={timeLeft.total}
    />
  );
};

type TimerTextProps = {
  minutes: number;
  seconds: number;
  total: number;
};

const TimerText = ({ minutes, seconds, total }: TimerTextProps) => {
  return (
    <div className="flex flex-col space-y-2 w-full">
      <p className="text-white w-full h-fit">New price list in:</p>
      <p className="text-2xl text-white w-full h-fit font-semibold">
        {total > 0
          ? `${minutes < 10 ? "0" + minutes : minutes}:${seconds < 10 ? "0" + seconds : seconds}`
          : "Refresh your search to get new valid routes"}
      </p>
    </div>
  );
};
