import React from "react";
import { cn } from "@/lib/utils"; // or replace with your own class combiner

type FetchedContentContainerProps = {
  isLoading: boolean;
  data: any[] | null | undefined;
  children: React.ReactNode;
};

export const FetchedContentContainer = ({ isLoading, data, children }: FetchedContentContainerProps) => {
  return (
    <div
      className={cn(
        "relative grid grid-cols-1 md:grid-cols-2 w-full gap-4 py-6 px-4 bg-card rounded border transition-opacity duration-200",
        isLoading && "opacity-50 pointer-events-none"
      )}
    >
      {isLoading && (
        <div className="absolute inset-0 flex items-center justify-center z-10">
          <LoadingSpinner />
        </div>
      )}

      {!isLoading && (!data || data.length === 0) ? (
        <p className="w-full text-center text-muted-foreground col-span-2">No data found.</p>
      ) : (
        children
      )}
    </div>
  );
};

const LoadingSpinner = () => (
  <div className="w-8 h-8 border-4 border-muted border-t-primary rounded-full animate-spin" />
);
