import { useEffect, useState } from "react";
import { useQueryClient } from "@tanstack/react-query";
import type { ValidUntil } from "@/pages/RoutesPage";

type TimeLeft = {
  total: number;
  minutes: number;
  seconds: number;
};

export const PriceListValidTimer = () => {
  const queryClient = useQueryClient();
  const validUntil: ValidUntil | undefined = queryClient.getQueryData([
    "validUntil",
  ]);

  const [timeLeft, setTimeLeft] = useState<TimeLeft | null>(null);

  useEffect(() => {
    if (!validUntil) return;

    const target = new Date(validUntil.validUntil + "Z");
    let interval: ReturnType<typeof setInterval>;

    const updateTime = () => {
      const remaining = getTimeRemaining(target);
      setTimeLeft(remaining);

      if (remaining.total <= 0) {
        clearInterval(interval);
      }
    };

    updateTime();
    interval = setInterval(updateTime, 1000);

    return () => clearInterval(interval);
  }, [validUntil]);

  if (!validUntil) return <span>Loading...</span>;
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

function getTimeRemaining(targetDate: Date): TimeLeft {
  const total = targetDate.getTime() - new Date().getTime();

  const seconds = Math.max(Math.floor((total / 1000) % 60), 0);
  const minutes = Math.max(Math.floor((total / 1000 / 60) % 60), 0);

  return {
    total,
    minutes,
    seconds,
  };
}
